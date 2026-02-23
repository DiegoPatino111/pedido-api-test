using Dapper;
using processOrderApi.Helpers;
using processOrderApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace processOrderApi.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public LogRepository(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task RegistrarEventoAsync(LogAuditoria log)
        {
            using (var conn = _dbHelper.ObtenerConexion())
            {
                await conn.OpenAsync();
                const string sql = @"
                INSERT INTO LogAuditoria (Evento, Descripcion, Nivel, Usuario)
                VALUES (@Evento, @Descripcion, @Nivel, @Usuario)";

                await conn.ExecuteAsync(sql, log);
            }
        }

        
        public async Task RegistrarEventoAsync(LogAuditoria log, IDbTransaction transaction)
        {
            const string sql = @"
            INSERT INTO LogAuditoria (Evento, Descripcion, Nivel, Usuario)
            VALUES (@Evento, @Descripcion, @Nivel, @Usuario)";

            await transaction.Connection.ExecuteAsync(sql, log, transaction);
        }
    }
}