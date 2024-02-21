using FluentValidation.Results;
using MonthlyCosts.Domain.Core.Commands;
using MonthlyCosts.Domain.Entities;

namespace MonthlyCosts.Domain.Commands;

public class UpdateCostValueCommand : Command
{
    public Guid Id { get; set; }
    public Guid CostId { get; set; }
    public decimal Value { get; set; }
    public DateOnly Month { get; set; }

    public override ValidationResult Validate()
    {
        return base.ValidationResult;
    }
}
