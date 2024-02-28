using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MonthlyCosts.Domain.Core.Bus;
using MonthlyCosts.Domain.Events;
using MonthlyCosts.Domain.Settings;
using MonthlyCosts.Infra.Bus;
using System.Reflection;

namespace MonthlyCosts.Infra.IoC;

public static class MessageBrokerSetupExtension
{
    public static void AddMessageBrokerInMemmory(this IServiceCollection services)
    {
        services.AddMediatR(p => p.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<IMediatorHandler, InMemoryBus>();
    }

    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(EventBusSettings.SectionName).Get<EventBusSettings>()
            ?? throw new NullReferenceException($"Missing #{nameof(EventBusSettings)} on the app settings");

        services.AddSingleton<IRabbitMQEventBus, RabbitMQEventBus>();
        services.AddSingleton<IRabbitMQPersistentConnection>(sp => {
            var logger = sp.GetRequiredService<ILogger<RabbitMQPersistentConnection>>();
            return new RabbitMQPersistentConnection(settings, logger);
        });
        services.AddSingleton<IRabbitMQConsumer, RabbitMQConsumer>();

        return services;
    }
    public static IApplicationBuilder ConfigureRabbitMq(this IApplicationBuilder app)
    {
        var listener = app.ApplicationServices.GetService<IRabbitMQConsumer>();

        listener.Subscribe<CreateCostEvent>();
        listener.Subscribe<UpdateCostEvent>();
        listener.Subscribe<DeleteCostEvent>();
        listener.Subscribe<CreateCostValueEvent>();
        listener.Subscribe<UpdateCostValueEvent>();
        listener.Subscribe<DeleteCostValueEvent>();

        return app;
    }
}
