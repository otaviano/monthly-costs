namespace MonthlyCosts.Domain.Entities
{
    public class Cost
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal Avarage { get; set; }
        public decimal Value { get; set; }
        public DateOnly Month { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}