using MonthlyCosts.Domain.Core.Events;

namespace MonthlyCosts.Domain.Events;

public class UpdateCostValueEvent : Event
{
    public Guid Id { get; set; }
    public Guid CostId { get; set; }
    public decimal Value { get; set; }
    public DateOnly Month { get; set; }
}
