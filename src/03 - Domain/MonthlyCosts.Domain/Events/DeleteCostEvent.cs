using MediatR;
using MonthlyCosts.Domain.Core.Events;

namespace MonthlyCosts.Domain.Events;

public class DeleteCostEvent : Event
{
    public Guid Id { get; set; }
}
