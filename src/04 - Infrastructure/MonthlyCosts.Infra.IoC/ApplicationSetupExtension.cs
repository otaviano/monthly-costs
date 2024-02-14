using Microsoft.Extensions.DependencyInjection;
using MonthlyCost.Application;
using MonthlyCost.Application.Interfaces;

namespace MonthlyCosts.Infra.IoC
{
    public static class ApplicationSetupExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICostApplication, CostApplication>();
        }
    }
}
