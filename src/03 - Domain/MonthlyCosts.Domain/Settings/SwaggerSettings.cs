
namespace MonthlyCosts.Domain.Settings
{
    public class SwaggerSettings
    {
        public const string SectionName = nameof(SwaggerSettings);

        public IFormatProvider? Url { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
