﻿using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MonthlyCosts.Domain.Interfaces;
using MonthlyCosts.Infra.Data.MongoDb;
using MonthlyCosts.Infra.Data.SqlServer;
using Stocks.Billing.Infra.Data.MongoDb.Context;

namespace MonthlyCosts.Infra.IoC;

public static class DataSetupExtension
{
    public static IServiceCollection AddSqlConnection(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(DbContext).Assembly;

        services.AddTransient(sp => DbContext.CreateConnection(configuration));
        services.AddFluentMigratorCore()
            .ConfigureRunner(p =>
                p.AddSqlServer()
                    .WithGlobalConnectionString(configuration.GetConnectionString("DbConnection"))
                    .ScanIn(assembly)
                    .For.All())
            .AddLogging(p => p.AddFluentMigratorConsole());

        return services;
    }
    public static IServiceCollection AddNoSqlConnection(this IServiceCollection services)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

        services.AddSingleton(typeof(MongoDbContext<>));

        return services;
    }
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<ICostNoSqlRepository, CostMongoDbRepository>();
        services.AddTransient<ICostValueNoSqlRepository, CostValueMongoDbRepository>();
        services.AddTransient<ICostSqlRepository, CostSqlServerRepository>();
        services.AddTransient<ICostValueSqlRepository, CostValueSqlServerRepository>();

        return services;
    }
    public static IApplicationBuilder ConfigureMigrations(this IApplicationBuilder host)
    {
        using var scope = host.ApplicationServices.CreateScope();
        var migration = scope.ServiceProvider.GetService<IMigrationRunner>();

        try
        {
            migration.ListMigrations();
            migration.MigrateUp();
        }
        catch (Exception e)
        {
            migration.Rollback(1);
            Console.WriteLine($"Error running migrations: \r\n {e}");
            throw;
        }

        return host;
    }
}
