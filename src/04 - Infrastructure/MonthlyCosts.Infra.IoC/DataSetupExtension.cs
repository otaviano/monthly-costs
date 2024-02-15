using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MonthlyCosts.Domain.Interfaces;
using MonthlyCosts.Infra.Data.MongoDb;
using Stocks.Billing.Infra.Data.MongoDb.Context;

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
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

        services.AddSingleton(typeof(MongoDbContext<>));
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICostNoSqlRepository, CostMongoDbRepository>();
    }
}
