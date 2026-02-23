using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace processOrderApi.Models
{
    public class LogAuditoria
    {
        public long Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Evento { get; set; }
        public string Descripcion { get; set; }
        public string Nivel { get; set; } = "INFO";
        public string Usuario { get; set; }
    }
}