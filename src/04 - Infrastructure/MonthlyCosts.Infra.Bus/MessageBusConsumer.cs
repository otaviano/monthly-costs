using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MonthlyCosts.Domain.Core.Bus;
using MonthlyCosts.Domain.Entities;
using MonthlyCosts.Domain.Settings;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core.Tokenizer;

namespace MonthlyCosts.Infra.Bus;

public class MessageBusConsumer : IHostedService
{
    private readonly MessageBusSettings _settings;
    private readonly ServiceBusClient _queueClient;
    private readonly IServiceProvider _serviceProvider;
    private ServiceBusProcessor _serviceBusProcessor;

    public MessageBusConsumer(
        IServiceProvider serviceProvider,
        MessageBusSettings settings)
    {
        _settings = settings;
        _serviceProvider = serviceProvider;
        _queueClient = new ServiceBusClient(settings.ConnectionString);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await ProcessMessageHandlerAsync();
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _serviceBusProcessor.DisposeAsync();
        await _queueClient.DisposeAsync();
    }

    private async Task ProcessMessageHandlerAsync()
    {
        var options = new ServiceBusProcessorOptions
        {
            AutoCompleteMessages = false,
            MaxConcurrentCalls = 1
        };

        _serviceBusProcessor = _queueClient.CreateProcessor(_settings.QueueName, options);
        _serviceBusProcessor.ProcessMessageAsync += MessageHandler;
        _serviceBusProcessor.ProcessErrorAsync += ErrorHandler;

        await _serviceBusProcessor.StartProcessingAsync();
    }

    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        var messageBody = args.Message.Body.ToString();
        var eventName = args.Message.ContentType;
        var @event = GetEventType(eventName, messageBody);
        Console.WriteLine($"Received message: {eventName} ");

        using var scope = _serviceProvider.CreateScope();
        var mediatorHandler = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
        await mediatorHandler.SendEvent(@event);
        
        Console.WriteLine($"Processed message: {eventName} ");
        await args.CompleteMessageAsync(args.Message);
    }

    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine($"Message handler encountered an exception {args.Exception}.");
        Console.WriteLine(args.ErrorSource);
        Console.WriteLine(args.FullyQualifiedNamespace);
        Console.WriteLine($"- Entity Path: {args.EntityPath}");

        return Task.CompletedTask;
    }

    private static dynamic GetEventType(string eventName, string message)
    {
        var assembly = typeof(Cost).Assembly;

        Type eventType = assembly.GetType("MonthlyCosts.Domain.Events." + eventName);
        var obj = JsonConvert.DeserializeObject(message, eventType);
        dynamic @event = Convert.ChangeType(obj, eventType);

        return @event;
    }
}
