using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonthlyCosts.Domain.Core.Bus;
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

    public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IServiceBusClientFactory, ServiceBusClientFactory>();
        services.AddTransient<IMessageBusPublisher, MessageBusPublisher>();
        services.AddHostedService<MessageBusConsumer>();

        return services;
    }
}
