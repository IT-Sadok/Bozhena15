using FluentValidation;
using SmartHouseManagment.Api.Middleware.Models;

namespace SmartHouseManagment.Api.Middleware;

public class ExceptionHandlingMiddleware(
    ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }    
    }

    private async Task HandleException(HttpContext context, Exception exception)
    {
        var (statusCode, message) = exception switch
        {
            ValidationException ex    => (StatusCodes.Status400BadRequest, ex.Message),
            _                         => (StatusCodes.Status500InternalServerError, "Internal server error")
        };
        
        if (exception is InvalidOperationException)
        {
            logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);
        }
        
        await context.Response.WriteAsJsonAsync(new ErrorResponse(statusCode, message));
    }
}