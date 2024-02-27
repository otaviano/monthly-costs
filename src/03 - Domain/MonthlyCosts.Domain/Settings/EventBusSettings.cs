namespace MonthlyCosts.Domain.Settings;

public class EventBusSettings
{
    public const string SectionName = nameof(EventBusSettings);

    public string EventBusHostname { get; set; }
    public string EventBusUsername { get; set; }
    public string EventBusPassword { get; set; }
    public int? RetryCount { get; set; }
    public string QueueName { get; set; }
    public string BrokerName { get; set; } 


}
