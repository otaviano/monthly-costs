using FluentValidation.Results;
using System.Diagnostics.CodeAnalysis;

namespace MonthlyCosts.API.Filters;

[ExcludeFromCodeCoverage]
public class JsonErrorResponse
{
    public IReadOnlyList<JsonError> Errors { get; set; }

    public JsonErrorResponse() { }

    public JsonErrorResponse(string errorMessage)
    {
        var errors = new List<JsonError>(capacity: 1) { new JsonError(errorMessage) };

        Errors = errors;
    }

    public JsonErrorResponse(JsonError error)
    {
        var errors = new List<JsonError>(capacity: 1) { error };

        Errors = errors;
    }

    public JsonErrorResponse(IEnumerable<ValidationFailure> failures)
    {
        var errors = new List<JsonError>(capacity: failures.Count());

        foreach (var failure in failures)
        {
            errors.Add(new JsonError(failure.ErrorMessage, failure.ErrorCode));
        }

        Errors = errors;
    }

    public JsonErrorResponse(IReadOnlyList<string> errorMessages)
    {
        var errors = new List<JsonError>(capacity: errorMessages.Count);

        foreach (var errorResponse in errorMessages)
        {
            errors.Add(JsonError.FromCorreiosValidationError(errorResponse));
        }

        Errors = errors;
    }
}
