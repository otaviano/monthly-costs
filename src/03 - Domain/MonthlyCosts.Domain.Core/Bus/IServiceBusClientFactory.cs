using Azure.Messaging.ServiceBus;

namespace MonthlyCosts.Domain.Core.Bus;

public interface IServiceBusClientFactory
{
    ServiceBusClient CreateClient();
}
