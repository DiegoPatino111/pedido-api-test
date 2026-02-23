using processOrderApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace processOrderApi.Repositories
{
    public interface ILogRepository
    {
        
        Task RegistrarEventoAsync(LogAuditoria log);

        
        Task RegistrarEventoAsync(LogAuditoria log, IDbTransaction transaction);
    }
}