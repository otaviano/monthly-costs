using Microsoft.Extensions.DependencyInjection;
using MonthlyCosts.Infra.Bus;
using MonthlyCosts.Domain.Core.Bus;
using System.Reflection;

namespace MonthlyCosts.Infra.IoC;

public static class MessageBrokerSetupExtension
{
    public static void AddMessageBrokerInMemmory(this IServiceCollection services)
    {
        services.AddMediatR(p => p.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<IMediatorHandler, InMemoryBus>();
    }
}
