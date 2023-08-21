using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMigrator
{
    public class MigratorRunner
    {
        private readonly string _connectionString;

        public MigratorRunner(string connection)
        {
            _connectionString = connection;
        }

        public void Migrate()
        {
            var service = CreateService();

            using(var scope = service.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider.GetRequiredService<IMigrationRunner>());
            }
        }

        public IServiceProvider CreateService()
        {
            Console.WriteLine(typeof(MigratorRunner).Assembly.FullName);

            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb.AddPostgres().WithVersionTable(new VersionTable())
                    .WithGlobalConnectionString(_connectionString)
                    .ScanIn(typeof(MigratorRunner).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private void UpdateDatabase(IMigrationRunner runner)
        {
            runner.MigrateUp();

            using(var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                connection.ReloadTypes();
            }
        }
    }
}
