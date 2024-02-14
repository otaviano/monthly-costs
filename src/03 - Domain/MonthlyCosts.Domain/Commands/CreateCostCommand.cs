using FluentValidation.Results;
using MonthlyCosts.Domain.Core.Commands;
using MonthlyCosts.Domain.Entities;

namespace MonthlyCosts.Domain.Commands;

public class CreateCostCommand : Command
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public decimal Avarage { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

    public override ValidationResult Validate()
    {
        return base.ValidationResult;
    }
}
