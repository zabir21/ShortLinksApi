using DapperRelization.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Testcontainers.PostgreSql;

namespace ShortLinkTest.Fixtures
{
    public class DockerWebApplicationFactoryFixture : WebApplicationFactory<Program>, IAsyncLifetime
    {

        private PostgreSqlContainer _dbContainer;
        public int InitialUsersCount { get; } = 3;

        public DockerWebApplicationFactoryFixture()
        {
            _dbContainer = new PostgreSqlBuilder().Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var connectionString = _dbContainer.GetConnectionString();
            base.ConfigureWebHost(builder);
            var configuration = GetConfiguration();

            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT",
                string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI")) ? "Local" : "Testing");

            DbMigrator.Program.Main(new[]
            {
                $"--connection", connectionString
            });


            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(IDbConnectionFactory));
                services.AddScoped<IDbConnectionFactory, DbConnectionFactory>(s => new DbConnectionFactory(connectionString));
            });

            Console.Write($"Postgressql: {connectionString}");
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
        }

        public async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
            await _dbContainer.DisposeAsync();
            await base.DisposeAsync();
        }

        public IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
