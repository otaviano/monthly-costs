using System.Diagnostics.CodeAnalysis;

namespace MonthlyCosts.API.Filters;

[ExcludeFromCodeCoverage]
public class JsonError
{
    public JsonError() { }

    public JsonError(string message, string code = null)
    {
        Message = message;
        Code = code;
    }

    public string Code { get; set; }

    public string Message { get; set; }

    public static JsonError FromCorreiosValidationError(string errorMessage)
    {
        if (string.IsNullOrEmpty(errorMessage))
            return null;

        var separatorIndex = errorMessage.IndexOf(':');
        if (separatorIndex < 0)
            return new JsonError(errorMessage);

        return new JsonError(
            code: errorMessage[..separatorIndex].Trim(),
            message: errorMessage[(separatorIndex + 1)..].Trim());
    }
}