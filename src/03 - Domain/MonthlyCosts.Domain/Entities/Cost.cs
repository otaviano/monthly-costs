using MongoDB.Bson.Serialization.Attributes;

namespace MonthlyCosts.Domain.Entities;

public class Cost
{
    [BsonId]
    public Guid Id { get; set; }
    [BsonElement("Name")]
    public string Name { get; set; }
    public decimal Avarage { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}