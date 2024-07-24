using Genial.CashFlow.Infrastructure.Data;
using Genial.CashFlow.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Genial.CashFlow.Infrastructure
{
    public static class BusinessExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<DapperContext>();
            services.AddSingleton<Database>();

            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IBalanceRepository, BalanceRepository>();

            return services;
        }
    }
}
