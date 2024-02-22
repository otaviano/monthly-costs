using MongoDB.Bson.Serialization.Attributes;

namespace MonthlyCost.Application.ViewModels.v1;

public class CostValueSumResponseViewModel
{
    public decimal TotalValue { get; set; }
}
