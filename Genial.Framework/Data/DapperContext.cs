using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.Framework.Data
{
    public class DapperContext
    {
        private readonly IConfiguration configuration;
        private readonly string databaseName;
        private readonly string connectionStringName;

        public DapperContext(IConfiguration configuration, string databaseName, string connectionStringName = "DefaultConnection")
        {
            this.configuration = configuration;
            this.databaseName = databaseName;
            this.connectionStringName = connectionStringName;
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(this.configuration.GetConnectionString(this.connectionStringName));
        public IDbConnection CreateMasterConnection()
            => new SqlConnection(this.configuration.GetConnectionString(this.connectionStringName)?.Replace(databaseName, "master"));

        public void EnsureDatabaseExists()
        {
            var query = "SELECT * FROM sys.databases WHERE name = @name";
            var parameters = new DynamicParameters();
            parameters.Add("name", this.databaseName);
            using (var connection = this.CreateMasterConnection())
            {
                var records = connection.Query(query, parameters);
                if (!records.Any())
                    connection.Execute($"CREATE DATABASE {this.databaseName}");
            }
        }
    }
}
