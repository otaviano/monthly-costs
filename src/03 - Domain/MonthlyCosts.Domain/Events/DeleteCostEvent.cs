using MediatR;
using MonthlyCosts.Domain.Core.Events;
using MonthlyCosts.Domain.Entities;

namespace MonthlyCosts.Domain.Events;

public class DeleteCostEvent : Event<DeleteCostEvent>, IRequest
{
    public Guid Id { get; set; }
}
