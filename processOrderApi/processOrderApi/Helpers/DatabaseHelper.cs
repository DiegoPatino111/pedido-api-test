using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace processOrderApi.Helpers
{
    public class DatabaseHelper
    {
        public IDbConnection ObtenerConexion()
        {
            return new SqlConnection(
                ConfigurationManager.ConnectionStrings["PedidoDB"].ConnectionString);
        }
    }
}