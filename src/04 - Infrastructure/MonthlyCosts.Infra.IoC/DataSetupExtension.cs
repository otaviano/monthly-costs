using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonthlyCosts.Domain.Interfaces;
using MonthlyCosts.Infra.Data.NoSql;
using Stocks.Billing.Infra.Data.NoSql.Context;

namespace MonthlyCosts.Infra.IoC;

public static class DataSetupExtension
{
    public static void AddSqlConnection(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<DbContext>(p =>
        //{
        //    p.UseSqlServer(configuration.GetConnectionString("DbConnection"));
        //});
    }

    public static void AddNoSqlConnection(this IServiceCollection services)
    {
        services.AddSingleton(typeof(MongoDbContext<>));
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICostNoSqlRepository, CostMongoDbRepository>();
    }
}
