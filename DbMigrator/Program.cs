using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data;

namespace DbMigrator;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Contains("--dryrun"))
        {
            return;
        }

        CommandLineOptions? options = null;

        if (args != null && args.Length > 0)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed<CommandLineOptions>(c =>
                {
                    Console.WriteLine(c.ConnectionString);

                    options = c;
                });
        }

        MigrateDatabase(options?.ConnectionString);
    }

    static void MigrateDatabase(string? connectionString)
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ??
            throw new InvalidOperationException("ASPNETCORE_ENVIRONMENT is not set");

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{environmentName}.json")
            .Build();

        connectionString ??= config["ConnectionStrings:DefaultConnection"];
        var migrationRunner = new MigratorRunner(connectionString);
        migrationRunner.Migrate();
    }
}