using Dapper;
using processOrderApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace processOrderApi.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        public async Task<int> InsertarCabeceraAsync(PedidoCabecera cabecera, IDbTransaction transaction)
        {
            const string sql = @"
            INSERT INTO PedidoCabecera (ClienteId, Fecha, Total, Usuario, Estado)
            VALUES (@ClienteId, @Fecha, @Total, @Usuario, @Estado);
            SELECT CAST(SCOPE_IDENTITY() AS INT)";

            return await transaction.Connection.QuerySingleAsync<int>(sql, cabecera, transaction);
        }

        public async Task InsertarDetalleAsync(PedidoDetalle detalle, IDbTransaction transaction)
        {
            const string sql = @"
            INSERT INTO PedidoDetalle (PedidoId, ProductoId, Cantidad, Precio)
            VALUES (@PedidoId, @ProductoId, @Cantidad, @Precio)";

            await transaction.Connection.ExecuteAsync(sql, detalle, transaction);
        }
    }
}