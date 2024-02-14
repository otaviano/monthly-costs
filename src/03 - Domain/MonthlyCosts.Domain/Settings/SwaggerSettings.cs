
namespace MonthlyCosts.Domain.Settings;

public class SwaggerSettings
{
    public const string SectionName = nameof(SwaggerSettings);

    public string Url { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
