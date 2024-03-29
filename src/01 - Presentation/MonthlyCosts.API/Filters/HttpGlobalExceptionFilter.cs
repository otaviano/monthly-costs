﻿using AutoMapper;
using FluentValidation;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MonthlyCosts.Domain.Core.Exceptions;
using System.Security.Authentication;

namespace MonthlyCosts.API.Filters;

public class HttpGlobalExceptionFilter : IExceptionFilter
{
    private readonly IHostEnvironment _env;
    private readonly ILogger<HttpGlobalExceptionFilter> _logger;

    public HttpGlobalExceptionFilter(IHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
    {
        _env = env;
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var exception = GetCurrentException(context);

        _logger?.LogError(exception, exception.Message);

        int statusCode = StatusCodes.Status500InternalServerError;
        string message = "Error processing the request.";

        switch (exception)
        {
            case NoMatchFoundException noMatchFoundException:
                message = noMatchFoundException.Message;
                statusCode = StatusCodes.Status422UnprocessableEntity;
                break;
            case InvalidCommandException invalidCommandException:
                message = invalidCommandException.Message;
                statusCode = StatusCodes.Status422UnprocessableEntity;
                break;
            case ValidationException validationException:
                message = validationException.Message;
                statusCode = StatusCodes.Status422UnprocessableEntity;
                break;
            case AuthenticationException authenticationException:
                message = authenticationException.Message;
                statusCode = StatusCodes.Status401Unauthorized;
                break;
            case NotImplementedException _:
                statusCode = StatusCodes.Status501NotImplemented;
                break;
        }

        var json = GetJsonErrorResponse(exception, message, statusCode);

        if (_env.IsDevelopment()
            && exception is not ValidationException)
        {
            json.Errors[0].Message = exception.ToString();
        }

        context.Result = new ObjectResult(json) { StatusCode = statusCode };
        context.HttpContext.Response.StatusCode = statusCode;
        context.ExceptionHandled = true;
    }

    private static Exception GetCurrentException(ExceptionContext context)
    {
        var exception = context.Exception;

        if (exception is AggregateException && exception.InnerException != null)
        {
            exception = exception.InnerException;
        }

        if (exception is AutoMapperMappingException
            && exception.InnerException is NoMatchFoundException)
        {
            exception = exception.InnerException;
        }

        return exception;
    }

    public JsonErrorResponse GetJsonErrorResponse(Exception exception, string message, int statusCode)
    {
        if (exception is ValidationException validationException)
        {
            return new JsonErrorResponse(validationException.Errors);
        }

        return new JsonErrorResponse(new JsonError(message, $"HTTP-{statusCode}"));
    }
}