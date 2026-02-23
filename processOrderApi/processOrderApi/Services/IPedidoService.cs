using processOrderApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace processOrderApi.Services
{
    public interface IPedidoService
    {
        Task<OrderProcessingResult> ProcesarPedidoAsync(CreatePedidoRequest request);
    }
}