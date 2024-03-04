namespace MonthlyCost.Application.ViewModels.v1;

public class CostResponseViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Avarage { get; set; }
    public string PaymentMethod { get; set; }
}
