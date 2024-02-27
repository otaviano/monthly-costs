namespace MonthlyCosts.Domain.Core.Events;

public interface IEvent
{
    Guid EventId { get; }
    string EventName { get; }
    DateTime OccurredOn { get; }
}
