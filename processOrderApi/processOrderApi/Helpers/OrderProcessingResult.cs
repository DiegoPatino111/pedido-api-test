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

        public OrderProcessingResult(int pedidoId)
            : this(pedidoId, 200, "Pedido registrado exitosamente") { }

        public OrderProcessingResult(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}