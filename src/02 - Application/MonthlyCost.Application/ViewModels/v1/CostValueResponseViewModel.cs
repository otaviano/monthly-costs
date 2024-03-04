namespace MonthlyCost.Application.ViewModels.v1;

public class CostValueResponseViewModel
{
    public Guid Id { get; set; }
    public Guid CostId { get; set; }
    public decimal Value { get; set; }
    public DateOnly Month { get; set; }
}
