namespace MonthlyCosts.Domain.Entities
{
    public class CostValue
    {
        public Guid Id { get; set; }
        public Cost Cost { get; set; }
        public decimal Value { get; set; }
        public DateOnly Month { get; set; }
    }
}