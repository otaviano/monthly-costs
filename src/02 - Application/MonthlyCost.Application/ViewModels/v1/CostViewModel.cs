using MonthlyCosts.Domain.Entities;

namespace MonthlyCost.Application.ViewModels.v1
{
    public class CostViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Avarage { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
