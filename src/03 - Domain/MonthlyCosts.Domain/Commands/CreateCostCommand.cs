using FluentValidation.Results;
using MonthlyCosts.Domain.Core.Commands;

namespace MonthlyCosts.Domain.Commands;

public class CreateCostCommand : Command
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public decimal Avarage { get; set; }
    public decimal Value { get; set; }
    public DateOnly Month { get; set; }
    public short PaymentMethod { get; set; }

    public override ValidationResult Validate()
    {
        throw new NotImplementedException();
    }
}
