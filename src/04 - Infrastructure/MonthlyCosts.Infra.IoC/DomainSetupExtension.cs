using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MonthlyCosts.Domain.Commands;
using MonthlyCosts.Domain.Events;
using MonthlyCosts.Domain.Services.CommandHandlers;
using MonthlyCosts.Domain.Services.EventHandlers;

namespace MonthlyCosts.Infra.IoC;

public static class DomainSetupExtension
{
    public static void AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<CreateCostCommand, Unit>, CostCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteCostCommand, Unit>, CostCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateCostCommand, Unit>, CostCommandHandler>();

        services.AddScoped<IRequestHandler<CreateCostValueCommand, Unit>, CostValueCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteCostValueCommand, Unit>, CostValueCommandHandler>();

        services.AddScoped<IRequestHandler<CreateCostEvent>, CostEventHandler>();
        services.AddScoped<IRequestHandler<UpdateCostEvent>, CostEventHandler>();
        services.AddScoped<IRequestHandler<DeleteCostEvent>, CostEventHandler>();

        services.AddScoped<IRequestHandler<CreateCostValueEvent>, CostValueEventHandler>();
        services.AddScoped<IRequestHandler<DeleteCostValueEvent>, CostValueEventHandler>();
    }
}
