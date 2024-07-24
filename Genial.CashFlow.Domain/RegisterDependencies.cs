using Genial.CashFlow.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Genial.CashFlow.Domain
{
    public static class RegisterDependencies
    {
        public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IBalanceService, BalanceService>();

            return services;
        }
    }
}
