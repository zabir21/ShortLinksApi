using DapperRelization.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ShortLinkTest.Fixtures
{
    public class WebApplicationFactoryFixture: IAsyncLifetime
    {
        private WebApplicationFactory<Program> _factory;

        public HttpClient Client { get; private set; }
        public int InitialUsersCount { get; set; } = 3;

        public WebApplicationFactoryFixture()
        {
            var configuration = GetConfiguration();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.RemoveAll(typeof(IDbConnectionFactory));
                    services.AddScoped<IDbConnectionFactory, DbConnectionFactory>(s => new DbConnectionFactory(connectionString));
                });
            });
            Client = _factory.CreateClient();
        }

        public IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        async Task IAsyncLifetime.DisposeAsync()
        { 
        }

        async Task IAsyncLifetime.InitializeAsync()
        {
        }
    }
}
