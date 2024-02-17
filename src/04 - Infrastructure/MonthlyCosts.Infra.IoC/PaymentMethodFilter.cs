using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using MonthlyCosts.Domain.Entities;
using Humanizer;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MonthlyCosts.Infra.IoC;

public class PaymentMethodFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.MemberInfo?.Name != "PaymentMethod")
            return;

        schema.Type = "string";
        schema.Enum = Enum.GetValues(typeof(PaymentMethod))
                        .Cast<PaymentMethod>()
                        .Select(name => new OpenApiString(name.Humanize()))
                        .Cast<IOpenApiAny>().ToList();
    }
}
