using Microsoft.Extensions.Logging;
using MonthlyCosts.Domain.Core.Events;
using MonthlyCosts.Domain.Settings;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using System.Text;

namespace MonthlyCosts.Domain.Core.Bus;


public class RabbitMQEventBus : IRabbitMQEventBus, IDisposable
{
    private const string RetryAfterErrorMsg = "Retrying message because of error {0}";
    private bool disposedValue = false;

    private readonly EventBusSettings _settings;
    private readonly IRabbitMQPersistentConnection _connection;
    private readonly ILogger<RabbitMQEventBus> _logger;
    private readonly static Dictionary<string, Type> _subsManager = new();

    public RabbitMQEventBus(
        IRabbitMQPersistentConnection connection,
        ILogger<RabbitMQEventBus> logger,
        EventBusSettings settings)
    {
        _settings = settings;
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void Publish(IEvent @event)
    {
        _connection.TryConnect();

        var policy = RetryPolicy.Handle<BrokerUnreachableException>()
            .Or<SocketException>()
            .WaitAndRetry(_settings.RetryCount ?? 5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
            {
                _logger.LogWarning(RetryAfterErrorMsg, ex.ToString());
                Console.WriteLine(RetryAfterErrorMsg, ex.ToString());
            });

        using var model = _connection.CreateModel();
        var eventName = @event.GetType().Name;
        var message = JsonConvert.SerializeObject(@event);
        var body = Encoding.UTF8.GetBytes(message);

        policy.Execute(Publish(model, eventName, message, body));
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (!disposing || disposedValue) return;

        _subsManager.Clear();
        disposedValue = true;
    }

    private Action Publish(IModel model, string eventName, string message, byte[] body)
    {
        return () =>
        {
            Console.WriteLine($"Publising message #{message}");

            var properties = model.CreateBasicProperties();
            properties.DeliveryMode = 2;
            properties.Persistent = false;

            model.ConfirmSelect();
            model.BasicPublish(exchange: _settings.BrokerName,
                         routingKey: eventName,
                         basicProperties: properties,
                         body: body);
            model.WaitForConfirmsOrDie();
            model.BasicAcks += (sender, eventArgs) =>
            {
                Console.WriteLine($"Message #{eventName} sent to RabbitMQ");
            };
        };
    }

}

