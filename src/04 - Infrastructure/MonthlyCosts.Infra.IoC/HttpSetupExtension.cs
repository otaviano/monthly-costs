using Microsoft.Extensions.DependencyInjection;

namespace MonthlyCosts.Infra.IoC;

public static class HttpSetupExtension
{
    public static void AddHttpConfiguration(this IServiceCollection services, params Type[] typeFilters)
    {
        services.AddControllers(options =>
        {
            foreach (var item in typeFilters)
            {
                options.Filters.Add(item);
            }
        });
    }
}
