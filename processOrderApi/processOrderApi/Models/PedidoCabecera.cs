using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace processOrderApi.Models
{
    public class PedidoCabecera
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
        public string Usuario { get; set; }
        public string Estado { get; set; } = "PROCESADO";
    }
}