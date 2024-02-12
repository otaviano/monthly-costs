using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace MonthlyCosts.Infra.IoC;

public static class ApiVersioningSetupExtension
{
    public static void AddApiVersion(this IServiceCollection services)
    {
        services.AddApiVersioning(p =>
        {
            p.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(p =>
        {
            p.GroupNameFormat = "'v'VVV";
            p.SubstituteApiVersionInUrl = true;
        });
    }
}
