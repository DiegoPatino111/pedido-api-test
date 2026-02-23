using processOrderApi.Interfaces;
using processOrderApi.Models;
using processOrderApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace processOrderApi.Controllers
{
    public class PedidosController : ApiController
    {
        private readonly IPedidoService _pedidoService;
        private readonly ILogger _logger;

        public PedidosController(IPedidoService pedidoService, ILogger logger)
        {
            _pedidoService = pedidoService;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/pedidos")]
        public async Task<IHttpActionResult> RegistrarPedido([FromBody] CreatePedidoRequest request)
        {
            // Validación de modelo
            if (request == null)
                return BadRequest("Solicitud inválida");

            if (request.Items == null || request.Items.Count == 0)
                return BadRequest("Debe incluir al menos un producto");

            try
            {
                var resultado = await _pedidoService.ProcesarPedidoAsync(request);
                return Ok(new { id = resultado.PedidoId });
            }
            catch (Exception ex)
            {               
                return InternalServerError();
            }
        }
    }
}
