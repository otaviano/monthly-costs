using MongoDB.Bson.Serialization.Attributes;

namespace MonthlyCosts.Domain.Entities
{
    public class CostValue
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement("Name")]
        public Cost Cost { get; set; }
        public decimal Value { get; set; }
        public DateOnly Month { get; set; }
    }
}