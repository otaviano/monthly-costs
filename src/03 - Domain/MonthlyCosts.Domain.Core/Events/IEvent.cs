namespace MonthlyCosts.Domain.Core.Events;

public interface IEvent
{
    Guid EventId { get; }
    DateTime OccurredOn { get; }
}
