using MediatR;
using Microsoft.Extensions.Logging;
using MonthlyCosts.Domain.Core.Events;
using MonthlyCosts.Domain.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Channels;

namespace MonthlyCosts.Domain.Core.Bus;


public class RabbitMQEventBus : IDisposable, IRabbitMQEventBus
{
    private bool disposedValue = false; // To detect redundant calls

    private readonly EventBusSettings _settings;
    private readonly IRabbitMQPersistentConnection _persistentConnection;
    private readonly ILogger<RabbitMQEventBus> _logger;
    private readonly static Dictionary<string, Type> _subsManager = new();

    public RabbitMQEventBus(
        //IRabbitMQPersistentConnection persistentConnection,
        ILogger<RabbitMQEventBus> logger,
        EventBusSettings settings)
    {
        _settings = settings;
        //_persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void Publish(IEvent @event)
    {
        string UserName = "guest";
        string Password = "guest";
        string HostName = "rabbitmq";

        var connectionFactory = new RabbitMQ.Client.ConnectionFactory()
        {
            UserName = UserName,
            Password = Password,
            HostName = HostName
        };
        var connection = connectionFactory.CreateConnection();
        var model = connection.CreateModel();
        var properties = model.CreateBasicProperties();
        properties.Persistent = true;
        
        model.ExchangeDeclare(exchange: _settings.BrokerName, type: "direct");
        model.QueueDeclare(queue: _settings.QueueName, exclusive: false);
        
        var message = JsonConvert.SerializeObject(@event);
        var messagebuffer = Encoding.UTF8.GetBytes(message);

        model.BasicPublish(_settings.BrokerName, @event.GetType().Name, properties, messagebuffer);
        Console.WriteLine("Message Sent");

        //if (!_persistentConnection.IsConnected)
        //{
        //    _persistentConnection.TryConnect();
        //}

        //var policy = RetryPolicy.Handle<BrokerUnreachableException>()
        //    .Or<SocketException>()
        //    .WaitAndRetry(_settings.RetryCount ?? 5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
        //    {
        //        _logger.LogWarning("Retrying message because of error {0}", ex.ToString());
        //    });

        //using var model = _persistentConnection.CreateModel();
        //var eventName = @event.GetType().Name;

        ////channel.ExchangeDeclare(exchange: _settings.BrokerName,
        ////                    type: "direct");

        //var message = JsonConvert.SerializeObject(@event);
        //var body = Encoding.UTF8.GetBytes(message);

        //policy.Execute(() =>
        //{
        //    Console.WriteLine($"Publising message #{message}");

        //    var properties = model.CreateBasicProperties();
        //    properties.DeliveryMode = 2;
        //    properties.Persistent = false;

        //    model.ConfirmSelect();
        //    model.BasicPublish(exchange: _settings.BrokerName,
        //                 routingKey: eventName,
        //                 basicProperties: properties,
        //                 body: body);
        //    model.WaitForConfirmsOrDie();
        //    model.BasicAcks += (sender, eventArgs) =>
        //    {
        //        Console.WriteLine($"Message #{eventName} sent to RabbitMQ");
        //    };
        //});
    }

    protected virtual void InternalDispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _subsManager.Clear();
            }
            disposedValue = true;
        }
    }

    void IDisposable.Dispose()
        => InternalDispose(true);
}

