using Dapper.FluentMap.Configuration;
using DapperRelization.Context.Models;

namespace DapperRelization.Configurations
{
    public class DapperTypeConfiguration
    {
        public static void ConfigureModels(FluentConventionConfiguration builder)
        {
            builder.ForEntity<ShortLinkModel>();
        }
    }
}
