using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.CashFlow.Infrastructure.Data
{
    public class Database
    {
        private readonly DapperContext context;

        public Database(DapperContext context)
        {
            this.context = context;
        }

        public void CreateDatabase(string dbName)
        {
            var query = "SELECT * FROM sys.databases WHERE name = @name";
            var parameters = new DynamicParameters();
            parameters.Add("name", dbName);
            using (var connection = context.CreateMasterConnection())
            {
                var records = connection.Query(query, parameters);
                if (!records.Any())
                    connection.Execute($"CREATE DATABASE {dbName}");
            }
        }
    }
}
