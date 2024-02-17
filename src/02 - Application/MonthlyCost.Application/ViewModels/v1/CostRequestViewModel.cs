using Enum = MonthlyCosts.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MonthlyCost.Application.ViewModels.v1;

public class CostRequestViewModel
{
    public string Name { get; set; }
    public decimal Avarage { get; set; }
    [DataType(nameof(Enum.PaymentMethod))]
    public string PaymentMethod { get; set; }
}
