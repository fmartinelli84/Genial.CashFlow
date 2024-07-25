using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator.Infrastructure;
using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentMigrator.Runner;
using System.Reflection;
using Microsoft.Extensions.Hosting;

namespace Genial.Framework.Data
{
    public static class DapperExtensions
    {
        public static IServiceCollection AddDapper(this IServiceCollection services, 
            IConfiguration configuration,
            string databaseName,
            string connectionStringName = "DefaultConnection")
        {
           return services.AddSingleton(serviceProvider => new DapperContext(serviceProvider.GetRequiredService<IConfiguration>(), databaseName, connectionStringName));
        }
    }
}
