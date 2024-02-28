using MonthlyCosts.Domain.Core.Events;

namespace MonthlyCosts.Domain.Events;

public class DeleteCostValueEvent : Event
{
    public Guid Id { get; set; }
}
