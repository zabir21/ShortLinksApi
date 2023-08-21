using DapperRelization.Repositories.Interfaces;
using DapperRelization.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using DapperRelization.Context;
using Microsoft.Extensions.Configuration;
using Dapper.FluentMap;
using DapperRelization.Configurations;

namespace DapperRelization
{
    public static class DependencyInjection
    {
        public static void ConfigureDal(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IDbConnectionFactory, DbConnectionFactory>(s => new DbConnectionFactory(builder.Configuration.GetConnectionString("DefaultConnection")!));
            builder.Services.AddScoped<IUrlRepository, UrlRepository>();

            FluentMapper.Initialize(config =>
            {
                DapperTypeConfiguration.ConfigureModels(
                    config.AddConvention<PropertyNameToSnakeCaseConvension>());
            });
        }

        
    }
}
