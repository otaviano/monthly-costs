using MonthlyCosts.Domain.Core.Events;

namespace MonthlyCosts.Domain.Core.Bus;

public interface IRabbitMQConsumer
{
    void Close();
    void Subscribe<T>() where T : IEvent;
}