using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using RealEstateManagement.Business.Dto;
using RealEstateManagement.API.Exceptions;

namespace RealEstateManagement.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new ErrorResponseDto
        {
            IsSucceed = false,
            Message = "An error occurred while processing your request.",
            TraceId = context.TraceIdentifier
        };

        switch (exception)
        {
            case NotFoundException notFoundEx:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Message = notFoundEx.Message;
                response.Details = "The requested resource was not found";
                break;

            case ValidationException validationEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Validation failed";
                response.ValidationErrors = validationEx.Errors;
                response.Details = exception.Message;
                break;

            case ConflictException conflictEx:
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                response.Message = conflictEx.Message;
                response.Details = "A conflict occurred with the existing resource";
                break;

            case BusinessException businessEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = businessEx.Message;
                response.Details = "Business logic validation failed";
                break;

            case ArgumentNullException argNullEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = $"Required parameter is missing: {argNullEx.ParamName}";
                response.Details = argNullEx.Message;
                break;

            case ArgumentException argEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Invalid argument provided";
                response.Details = argEx.Message;
                break;

            case InvalidOperationException invOpEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Invalid operation";
                response.Details = invOpEx.Message;
                break;

            case KeyNotFoundException keyNotEx:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Message = "Resource not found";
                response.Details = keyNotEx.Message;
                break;

            case UnauthorizedAccessException unAuthEx:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                response.Message = "Unauthorized access";
                response.Details = unAuthEx.Message;
                break;

            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = "Internal server error occurred";
                response.Details = exception.Message;
                break;
        }

        return context.Response.WriteAsJsonAsync(response);
    }
}

