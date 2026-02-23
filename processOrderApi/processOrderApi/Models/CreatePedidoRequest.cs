using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace processOrderApi.Models
{
    public class CreatePedidoRequest
    {
        public int ClienteId { get; set; }
        public string Usuario { get; set; }
        public List<PedidoItemRequest> Items { get; set; } = new List<PedidoItemRequest>();
    }

    public class PedidoItemRequest
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}