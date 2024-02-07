using FluentValidation.Results;
using MonthlyCosts.Domain.Core.Commands;
using MonthlyCosts.Domain.Entities;

namespace MonthlyCosts.Domain.Commands;

public class UpdateCostCommand : Command
{
    public Guid Id { get; set; }
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
