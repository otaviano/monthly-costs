using RabbitMQ.Client;

namespace MonthlyCosts.Domain.Core.Bus;

public interface IRabbitMQPersistentConnection
{
    IModel CreateModel();
    void Dispose();
    bool TryConnect();
}