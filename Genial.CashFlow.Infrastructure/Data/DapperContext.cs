using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genial.CashFlow.Infrastructure.Data
{
    public class DapperContext
    {
        private readonly IConfiguration configuration;

        public DapperContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(this.configuration.GetConnectionString("DefaultConnection"));
        public IDbConnection CreateMasterConnection()
            => new SqlConnection(this.configuration.GetConnectionString("MasterConnection"));
    }
}
