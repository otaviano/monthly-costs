using MediatR;

namespace MonthlyCosts.Domain.Core.Events;

public class Event : IEvent, IRequest
{
    public Guid EventId { get; set; }
    public DateTime OccurredOn { get; set; }

    public Event()
    {
        EventId = Guid.NewGuid();
        OccurredOn = DateTime.Now;
    }
}
