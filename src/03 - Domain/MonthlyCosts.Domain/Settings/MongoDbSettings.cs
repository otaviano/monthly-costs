namespace MonthlyCosts.Domain.Settings;

public class MongoDbSettings
{
    public const string SectionName = nameof(MongoDbSettings);

    public string ConnectionString { get; set; }
    public string Database { get; set; }
}