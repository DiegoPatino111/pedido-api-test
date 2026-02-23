using processOrderApi.Helpers;
using processOrderApi.Models;
using processOrderApi.Interfaces;
using processOrderApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace processOrderApi.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepo;
        private readonly ILogRepository _logRepo;
        private readonly IExternalValidationService _validationService;
        private readonly ILogger _logger;
        private readonly DatabaseHelper _dbHelper;

        public PedidoService(
            IPedidoRepository pedidoRepo,
            ILogRepository logRepo,
            IExternalValidationService validationService,
            ILogger logger,
            DatabaseHelper dbHelper)
        {
            _pedidoRepo = pedidoRepo;
            _logRepo = logRepo;
            _validationService = validationService;
            _logger = logger;
            _dbHelper = dbHelper;
        }

        public async Task<OrderProcessingResult> ProcesarPedidoAsync(CreatePedidoRequest request)
        {
            using (var conn = _dbHelper.ObtenerConexion())
            {
                await conn.OpenAsync();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Registrar inicio en auditoría (DENTRO de la transacción)
                        await _logRepo.RegistrarEventoAsync(new LogAuditoria
                        {
                            Evento = "INICIO_PEDIDO",
                            Descripcion = $"Iniciando registro para cliente {request.ClienteId}",
                            Usuario = request.Usuario
                        }, tran);

                        // 2. Validación externa con manejo de errores
                        bool clienteValido;
                        try
                        {
                            clienteValido = await _validationService.ValidarClienteAsync(request.ClienteId);
                        }
                        catch (Exception ex)
                        {
                            await _logRepo.RegistrarEventoAsync(new LogAuditoria
                            {
                                Evento = "ERROR_VALIDACION",
                                Descripcion = $"Fallo en servicio externo: {ex.Message}",
                                Nivel = "ERROR",
                                Usuario = request.Usuario
                            }, tran);

                            tran.Rollback();
                            _logger.LogError("Error al validar cliente con servicio externo", ex);
                            return new OrderProcessingResult(503, "Servicio de validación no disponible");
                        }

                        if (!clienteValido)
                        {
                            await _logRepo.RegistrarEventoAsync(new LogAuditoria
                            {
                                Evento = "CLIENTE_INVALIDO",
                                Descripcion = $"Cliente {request.ClienteId} no existe",
                                Nivel = "ERROR",
                                Usuario = request.Usuario
                            }, tran);

                            tran.Rollback();
                            return new OrderProcessingResult(400, "Cliente no válido");
                        }

                        // 3. Cálculo de total y validación de items
                        decimal total = request.Items.Sum(i => i.Cantidad * i.Precio);
                        if (total <= 0) throw new InvalidOperationException("Total inválido");

                        // 4. Registro de cabecera
                        var cabecera = new PedidoCabecera
                        {
                            ClienteId = request.ClienteId,
                            Total = total,
                            Usuario = request.Usuario
                        };
                        int pedidoId = await _pedidoRepo.InsertarCabeceraAsync(cabecera, tran);

                        // 5. Registro de detalles
                        foreach (var item in request.Items)
                        {
                            if (item.Cantidad <= 0)
                                throw new ArgumentException($"Cantidad inválida para producto {item.ProductoId}");

                            await _pedidoRepo.InsertarDetalleAsync(new PedidoDetalle
                            {
                                PedidoId = pedidoId,
                                ProductoId = item.ProductoId,
                                Cantidad = item.Cantidad,
                                Precio = item.Precio
                            }, tran);
                        }

                        // 6. Confirmación exitosa
                        await _logRepo.RegistrarEventoAsync(new LogAuditoria
                        {
                            Evento = "PEDIDO_EXITOSO",
                            Descripcion = $"Pedido {pedidoId} registrado exitosamente",
                            Usuario = request.Usuario
                        }, tran);

                        tran.Commit();
                        return new OrderProcessingResult(pedidoId);
                    }
                    catch (Exception ex)
                    {
                        
                        await _logRepo.RegistrarEventoAsync(new LogAuditoria
                        {
                            Evento = "ERROR_GENERAL",
                            Descripcion = $"Error no controlado: {ex.Message}",
                            Nivel = "ERROR",
                            Usuario = request.Usuario
                        }, tran);
                        tran.Rollback();
                        _logger.LogError("Error crítico al procesar pedido", ex);
                        throw new Exception("Error interno del sistema", ex);
                    }
                }
            }
        }
    }
}