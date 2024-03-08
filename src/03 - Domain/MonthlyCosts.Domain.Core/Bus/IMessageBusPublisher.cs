using MonthlyCosts.Domain.Core.Events;

namespace MonthlyCosts.Domain.Core.Bus;

public interface IMessageBusPublisher
{
    Task SendMessageAsync(IEvent @event);
}