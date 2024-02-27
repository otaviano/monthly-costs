using MediatR;

namespace MonthlyCosts.Domain.Core.Events;

public class Event<T> : IEvent 
{
    public Guid EventId { get; set; }
    public string EventName { get; set; }
    public DateTime OccurredOn { get; set; }

    public Event()
    {
        EventId = Guid.NewGuid();
        EventName = nameof(T);
        OccurredOn = DateTime.Now;
    }
}
