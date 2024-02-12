using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MonthlyCosts.Domain.Commands;
using MonthlyCosts.Domain.Services.CommandHandlers;

namespace MonthlyCosts.Infra.IoC;

public static class DomainSetupExtension
{
    public static void AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<CreateCostCommand, Unit>, CostCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteCostCommand, Unit>, CostCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateCostCommand, Unit>, CostCommandHandler>();
    }
}
