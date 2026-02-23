using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace processOrderApi.Helpers
{
    public class OrderProcessingResult
    {
        public bool IsSuccess => PedidoId.HasValue;
        public int? PedidoId { get; }
        public int StatusCode { get; }
        public string Message { get; }

        // Constructor para éxito (con ID de pedido)
        public OrderProcessingResult(int pedidoId)
            : this(pedidoId, 200, $"Pedido {pedidoId} registrado exitosamente") { }

        // Constructor para errores
        public OrderProcessingResult(int statusCode, string message)
            : this(null, statusCode, message) { }

        // Constructor privado que maneja todos los casos
        private OrderProcessingResult(int? pedidoId, int statusCode, string message)
        {
            PedidoId = pedidoId;
            StatusCode = statusCode;
            Message = message;
        }
    }
}