using Microsoft.Extensions.DependencyInjection;
using MonthlyCost.Application.MapperProfiles;
using MonthlyCost.Application.MapperProfiles.Resolvers;
using MonthlyCosts.Infra.IoC;

namespace MonthlyCosts.Infra.IoC;

public static class AutoMapperSetupExtension
{
    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddTransient<CreatePaymentMethodResolver>();
        services.AddAutoMapper(typeof(AutoMapperConfiguration));
        AutoMapperConfiguration.RegisterMappings();
    }
}
