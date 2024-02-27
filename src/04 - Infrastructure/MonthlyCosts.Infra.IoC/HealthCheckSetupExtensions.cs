using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MonthlyCosts.Domain.Settings;
using MonthlyCosts.Infra.IoC;
using System.Diagnostics.CodeAnalysis;

namespace MonthlyCosts.Infra.IoC;

[ExcludeFromCodeCoverage]
public static class HealthCheckSetupExtensions
{
    public static void ConfigureHealthCheckEndpoints(this IApplicationBuilder app, IConfiguration configuration)
    {
        var settings = configuration.GetSection(HealthCheckSettings.SectionName).Get<HealthCheckSettings>()
          ?? throw new NullReferenceException($"Missing #{nameof(HealthCheckSettings)} on the app settings");

        app.UseHealthChecks(settings.Url, new HealthCheckOptions()
        {
            Predicate = (_) => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse

        });

        app.UseHealthChecks(settings.UrlSelf, new HealthCheckOptions()
        {
            Predicate = r => r.Name.Contains("self"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
    }

    public static void AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(HealthCheckSettings.SectionName).Get<HealthCheckSettings>()
           ?? throw new NullReferenceException($"Missing #{nameof(HealthCheckSettings)} on the app settings");
        var eventBusSettings = configuration.GetSection(EventBusSettings.SectionName).Get<EventBusSettings>()
          ?? throw new NullReferenceException($"Missing #{nameof(EventBusSettings)} on the app settings");
        var mongoDbSettings = configuration.GetSection(MongoDbSettings.SectionName).Get<MongoDbSettings>()
           ?? throw new NullReferenceException($"Missing #{nameof(MongoDbSettings)} on the app settings");

        services.AddHealthChecks()
          .AddCheck("self", () => HealthCheckResult.Healthy(settings.HealtyText))
          .AddSqlServer(
            connectionString: configuration.GetConnectionString("DbConnection")
                ?? throw new NullReferenceException($"Missing Connection String on the app settings"),
            name: "Sql Server"
          )
          .AddMongoDb(
            mongodbConnectionString: mongoDbSettings.ConnectionString
                ?? throw new NullReferenceException($"Missing DbConnection in section #{nameof(MongoDbSettings)} on the app settings"),
            name: "MongoDb"
          ).AddRabbitMQ(
            rabbitConnectionString: $"amqps://#{eventBusSettings.EventBusUsername}:#{eventBusSettings.EventBusPassword}@#{eventBusSettings.EventBusHostname}:5672");
    }
}
