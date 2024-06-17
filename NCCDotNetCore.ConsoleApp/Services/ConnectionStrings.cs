using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCCDotNetCore.ConsoleApp.Services
{
    internal static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",//server name
            InitialCatalog = "DotNetTrainingBatch4",//database name
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true

        };
    }
}
