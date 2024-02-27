using MonthlyCosts.Domain.Core.Events;

namespace MonthlyCosts.Domain.Core.Bus;

public interface IRabbitMQEventBus
{
    void Publish(IEvent @event);
}