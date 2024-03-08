using Azure.Messaging.ServiceBus;
using MonthlyCosts.Domain.Core.Bus;
using MonthlyCosts.Domain.Core.Events;
using MonthlyCosts.Domain.Settings;
using Newtonsoft.Json;

namespace MonthlyCosts.Infra.Bus;

public class MessageBusPublisher : IMessageBusPublisher
{
    private readonly MessageBusSettings _settings;

    public MessageBusPublisher(MessageBusSettings settings)
    {
        _settings = settings;
    }

    public async Task SendMessageAsync(IEvent @event)
    {
        await using var client = new ServiceBusClient(_settings.ConnectionString);
        var sender = client.CreateSender(_settings.QueueName);
        var message = new ServiceBusMessage(JsonConvert.SerializeObject(@event))
        {
            ContentType = @event.GetType().Name,
            MessageId = @event.EventId.ToString()
        };

        await sender.SendMessageAsync(message);
        Console.WriteLine("Message sent successfully.");
    }
}
