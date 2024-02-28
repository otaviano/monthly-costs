using FluentValidation.Results;
using System.Net;
using System.Runtime.Serialization;

namespace MonthlyCosts.Domain.Core.Exceptions;
public abstract class BaseInvalidClientOperationException : Exception
{
    public ValidationResult Result { get; set; }

    public HttpStatusCode StatusCode { get; set; }

    protected BaseInvalidClientOperationException(ValidationResult validationResult, string message, HttpStatusCode statusCode, Exception innerException = null) : base(message)
    {
        Result = validationResult ?? new ValidationResult();
        StatusCode = statusCode;
    }

    protected BaseInvalidClientOperationException(ValidationResult validationResult, HttpStatusCode statusCode)
    {
        Result = validationResult ?? new ValidationResult();
        StatusCode = statusCode;
    }

    protected BaseInvalidClientOperationException(string message, HttpStatusCode statusCode, Exception innerException = null) : base(message)
    {
        StatusCode = statusCode;
    }

    protected BaseInvalidClientOperationException(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    protected BaseInvalidClientOperationException(SerializationInfo info, StreamingContext context, HttpStatusCode statusCode) 
        : base(info, context)
    {
        StatusCode = statusCode;
    }
}