using FluentValidation.Results;
using MonthlyCosts.Domain.Core.Commands;

namespace MonthlyCosts.Domain.Commands;

public class DeleteCostCommand : Command
{
    public Guid Id { get; set; }
   
    public override ValidationResult Validate()
    {
        return base.ValidationResult;
    }
}
