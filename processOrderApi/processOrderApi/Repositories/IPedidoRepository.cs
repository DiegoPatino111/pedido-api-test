using processOrderApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace processOrderApi.Repositories
{
    public interface IPedidoRepository
    {
        Task<int> InsertarCabeceraAsync(PedidoCabecera cabecera, IDbTransaction transaction);
        Task InsertarDetalleAsync(PedidoDetalle detalle, IDbTransaction transaction);
    }
}