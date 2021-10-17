using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CustomerOrders.DataAccess
{
    public class SqlServerConnectionProvider : IConnectionProvider
    {
        private readonly string _connectionString;

        public SqlServerConnectionProvider(IConfiguration configuration)
        {
            _connectionString = configuration["Database:ConnectionString"];
        }

        public IDbConnection GetDbConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
