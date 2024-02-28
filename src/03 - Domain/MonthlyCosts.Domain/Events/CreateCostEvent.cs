using MediatR;
using MonthlyCosts.Domain.Core.Events;
using MonthlyCosts.Domain.Entities;

namespace MonthlyCosts.Domain.Events;

public class CreateCostEvent : Event
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Avarage { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}
