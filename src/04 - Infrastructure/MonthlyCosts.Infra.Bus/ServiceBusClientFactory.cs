using Azure.Messaging.ServiceBus;
using MonthlyCosts.Domain.Core.Bus;
using MonthlyCosts.Domain.Settings;

namespace MonthlyCosts.Infra.Bus;

public class ServiceBusClientFactory : IServiceBusClientFactory
{
    private readonly MessageBusSettings _settings;

    public ServiceBusClientFactory(MessageBusSettings settings)
    {
        _settings = settings;
    }

    public ServiceBusClient CreateClient()
    {
        return new ServiceBusClient(_settings.ConnectionString);
    }
}
