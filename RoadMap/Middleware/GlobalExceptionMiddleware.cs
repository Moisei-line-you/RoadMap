using FluentValidation;
using System.Text.Json;
using RoadMap.Domain.Exceptions;

namespace RoadMap.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        
        if (ex is not (ValidationException or NotFoundException or BusinessException or DomainException))
        {
            _logger.LogError(ex, "Unhandled exception for {Method} {Path}", context.Request.Method, context.Request.Path);
        }
        
        switch (ex)
        {
            case ValidationException validationEx:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var errors = validationEx.Errors.Select(e => new 
                { 
                    Field = e.PropertyName, 
                    Message = e.ErrorMessage 
                });
                
                var validationResponse = new 
                { 
                    title = "Validation Error", 
                    status = 400,
                    errors = errors 
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(validationResponse));
                break;
            
            case NotFoundException:
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(new { message = ex.Message });
                break;

            case BusinessException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { message = ex.Message });
                break;

            case DomainException:
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                await context.Response.WriteAsJsonAsync(new { message = ex.Message });
                break;


            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new { message = "Server error" });
                break;
        }
    }
}