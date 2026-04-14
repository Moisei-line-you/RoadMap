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
        
        if (ex is AppException appEx)
        {
            context.Response.StatusCode = appEx.StatusCode;

            await context.Response.WriteAsJsonAsync(new
            {
                message = appEx.Message,
                status = appEx.StatusCode,
                traceId = context.TraceIdentifier
            });

            return;
        }
        
        _logger.LogError(ex,
            "Unhandled exception for {Method} {Path}. TraceId: {TraceId}",
            context.Request.Method,
            context.Request.Path,
            context.TraceIdentifier);

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await context.Response.WriteAsJsonAsync(new
        {
            message = "Internal server error",
            status = 500,
            traceId = context.TraceIdentifier
        });
    }
}