using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MonthlyCosts.Domain.Core.Bus;
using MonthlyCosts.Domain.Core.Events;
using MonthlyCosts.Domain.Events;
using MonthlyCosts.Domain.Settings;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MonthlyCosts.Infra.Bus;

public class RabbitMQConsumer : IRabbitMQConsumer
{
    private readonly IServiceProvider _serviceProvider;
    private IModel _channel;
    private readonly EventBusSettings _settings;
    private readonly IRabbitMQPersistentConnection _connection;
    private readonly static Dictionary<string, Type> _subsManager = new();

    public RabbitMQConsumer(
        IRabbitMQPersistentConnection connection,
        IServiceProvider serviceProvider,
        EventBusSettings settings)
    {
        _connection = connection;
        _settings = settings;
        _connection.TryConnect();
        _serviceProvider = serviceProvider;
        _channel = CreateConsumerChannel();
    }

    private IModel CreateConsumerChannel()
    {
        _connection.TryConnect();
        var channel = _connection.CreateModel();

        channel.ExchangeDeclare(exchange: _settings.BrokerName, type: "direct");
        channel.QueueDeclare(queue: _settings.QueueName, exclusive: false);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += ReceivedEvent;

        channel.BasicConsume(queue: _settings.QueueName,
                             autoAck: false,
                             consumer: consumer);

        channel.CallbackException += (sender, ea) =>
        {
            _channel.Dispose();
            _channel = CreateConsumerChannel();
        };

        return channel;
    }

    private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
    {
        var body = e.Body.ToArray();
        var @type = e.RoutingKey;
        var message = Encoding.UTF8.GetString(body);
        var @event = JsonConvert.DeserializeObject<CreateCostEvent>(message);

        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            await context.SendEvent(@event);
        }

        Console.WriteLine("Message Received: {0}", message);
    }

    public void Subscribe<T>() where T : IEvent
    {
        var eventName = typeof(T).Name;
        var containsKey = _subsManager.ContainsKey(eventName);
        if (!containsKey)
        {
            _subsManager.Add(eventName, typeof(T));
        }

        _connection.TryConnect();

        using var channel = _connection.CreateModel();
        channel.QueueBind(queue: _settings.QueueName,
                          exchange: _settings.BrokerName,
                          routingKey: eventName);
    }

    public void Close()
    {
        _connection.Dispose();
        _channel.Close();
    }
}
