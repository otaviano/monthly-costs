namespace MonthlyCosts.Domain.Settings;

public class MessageBusSettings
{
    public const string SectionName = nameof(MessageBusSettings);

    public string ConnectionString { get; set; }
    public int? RetryCount { get; set; }
    public string QueueName { get; set; }
}
