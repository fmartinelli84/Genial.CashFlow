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
    public static class FluentMigratorExtensions
    {
        public static IServiceCollection AddFluentMigrator(this IServiceCollection services, 
            IConfiguration configuration,
            Assembly assembly,
            string connectionStringName = "DefaultConnection")
        {
            return services.AddLogging(c => c.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(c => c.AddSqlServer2016()
                    .WithGlobalConnectionString(configuration.GetConnectionString(connectionStringName))
                    .ScanIn(assembly).For.Migrations());
        }

        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var dapperContext = scope.ServiceProvider.GetRequiredService<DapperContext>();
                var migrationRunner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                dapperContext.EnsureDatabaseExists();

                migrationRunner.ListMigrations();
                migrationRunner.MigrateUp();
            }

            return host;
        }


        public static TNext WithTrackable<TNext>(this TNext current)
            where TNext : ICreateTableColumnOptionOrWithColumnSyntax
        {
            return (TNext)current
                .WithReadOnlyTrackable()
                .WithColumn("ModifiedAtDate").AsDateTime().Nullable();
        }

        public static TNext WithReadOnlyTrackable<TNext>(this TNext current)
            where TNext : ICreateTableColumnOptionOrWithColumnSyntax
        {
            return (TNext)current
                .WithColumn("CreatedAtDate").AsDateTime().Nullable();
        }
    }
}
