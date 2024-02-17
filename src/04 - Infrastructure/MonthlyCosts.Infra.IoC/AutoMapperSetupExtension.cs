using Microsoft.Extensions.DependencyInjection;
using MonthlyCost.Application.MapperProfiles;
using MonthlyCosts.Infra.IoC;

namespace MonthlyCosts.Infra.IoC;

public static class AutoMapperSetupExtension
{
    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddTransient<PaymentMethodResolver>();
        services.AddAutoMapper(typeof(AutoMapperConfiguration));
        AutoMapperConfiguration.RegisterMappings();
    }
}
