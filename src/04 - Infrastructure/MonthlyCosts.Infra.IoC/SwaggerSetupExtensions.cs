using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using MonthlyCosts.Domain.Settings;
using Microsoft.AspNetCore.Mvc.Controllers;
using Azure;
using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;

namespace MonthlyCosts.Infra.IoC;

[ExcludeFromCodeCoverage]
public static class SwaggerSetupExtensions
{
    public static void ConfigureSwagger(this IApplicationBuilder app, IConfiguration configuration, IApiVersionDescriptionProvider provider)
    {
        var settings = configuration.GetSection(SwaggerSettings.SectionName).Get<SwaggerSettings>()
          ?? throw new NullReferenceException($"Missing #{nameof(SwaggerSettings)} on the app settings");

        app.UseSwagger();
        app.UseSwaggerUI(p =>
        {
            p.SupportedSubmitMethods(Array.Empty<SubmitMethod>());

            foreach (var description in provider.ApiVersionDescriptions)
            {
                p.SwaggerEndpoint(string.Format(settings.Url, description.GroupName), $"{settings.Name} - {description.GroupName.ToUpperInvariant()}");
            }
        });
        app.ConfigureHealthCheckEndpoints(configuration);
    }

    public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(SwaggerSettings.SectionName).Get<SwaggerSettings>()
          ?? throw new NullReferenceException($"Missing #{nameof(SwaggerSettings)} on the app settings");

        services.AddSwaggerGen(p =>
        {
            var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var description in provider.ApiVersionDescriptions)
            {
                p.SwaggerDoc(
                    description.GroupName,
                    new OpenApiInfo
                    {
                        Title = settings.Name,
                        Version = description.GroupName,
                        Description = settings.Description
                   });
            }
            p.SchemaFilter<PaymentMethodFilter>();
            p.UseInlineDefinitionsForEnums();
            p.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        });
    }
}
