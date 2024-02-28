using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using MonthlyCosts.Domain.Settings;

namespace MonthlyCosts.Domain.Core.Bus;

public class RabbitMQPersistentConnection : IRabbitMQPersistentConnection
{
    private const string ShutDownConnectionMessage = "A RabbitMQ connection is on shutdown. Trying to re-connect...";
    private const string TryingToConnectMessage = "RabbitMQ Client is trying to connect";
    private const string FatalErroMessage = "FATAL ERROR: RabbitMQ connections could not be created and opened";
    private const string ExceptionTryingConnectionMessage = "A RabbitMQ connection throw exception. Trying to re-connect...";
    private readonly IConnectionFactory _connectionFactory;
    private readonly ILogger<RabbitMQPersistentConnection> _logger;
    private readonly int _retryCount;
    private readonly object sync_root = new();
    private IConnection _connection;
    private bool _disposed;

    public RabbitMQPersistentConnection(EventBusSettings configuration, ILogger<RabbitMQPersistentConnection> logger)
    {
        _connectionFactory = CreateFactory(configuration);
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _retryCount = configuration.RetryCount ?? 5;
    }

    private bool IsConnected
    {
        get
        {
            return _connection != null && _connection.IsOpen && !_disposed;
        }
    }
    public IModel CreateModel()
    {
        if (!IsConnected)
        {
            throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
        }

        return _connection.CreateModel();
    }
    public bool TryConnect()
    {
        if (IsConnected) return true;

        _logger.LogInformation(TryingToConnectMessage);
        Console.WriteLine("RabbitMQ Client is trying to connect");

        lock (sync_root)
        {
            var policy = RetryPolicy.Handle<SocketException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    Console.WriteLine(ex);
                    _logger.LogWarning(ex.ToString());
                }
            );

            policy.Execute(() =>
            {
                _connection = _connectionFactory
                      .CreateConnection();
            });

            if (IsConnected)
            {
                _connection.ConnectionShutdown += OnConnectionShutdown;
                _connection.CallbackException += OnCallbackException;
                _connection.ConnectionBlocked += OnConnectionBlocked;

                _logger.LogInformation($"RabbitMQ persistent connection acquired a connection {_connection.Endpoint.HostName} and is subscribed to failure events");
                Console.WriteLine($"RabbitMQ persistent connection acquired a connection {_connection.Endpoint.HostName} and is subscribed to failure events");

                return true;
            }
            else
            {
                _logger.LogCritical(FatalErroMessage);
                Console.WriteLine("FATAL ERROR: RabbitMQ connections could not be created and opened");

                return false;
            }
        }
    }
    private static IConnectionFactory CreateFactory(EventBusSettings settings)
    {
        var factory = new ConnectionFactory()
        {
            HostName = settings.EventBusHostname,
            UserName = settings.EventBusUsername,
            Password = settings.EventBusPassword
        };

        return factory;
    }
    private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
    {
        if (_disposed) return;

        _logger.LogWarning(ExceptionTryingConnectionMessage);
        Console.WriteLine(ExceptionTryingConnectionMessage);

        TryConnect();
    }
    private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
    {
        if (_disposed) return;

        _logger.LogWarning(ShutDownConnectionMessage);
        Console.WriteLine(ShutDownConnectionMessage);

        TryConnect();
    }
    private void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
    {
        if (_disposed) return;

        _logger.LogWarning(ShutDownConnectionMessage);
        Console.WriteLine(ShutDownConnectionMessage);

        TryConnect();
    }

    public void Dispose()
    {
        if (_disposed) return;

        _disposed = true;

        try
        {
            _connection.Dispose();
        }
        catch (IOException ex)
        {
            _logger.LogCritical(ex.ToString());
            Console.WriteLine(ex);
        }
    }
}
