using RoadMap.Domain.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            if (ex is not (NotFoundException or BusinessException or DomainException))
                _logger.LogError(ex, "Unhandled exception for {Method} {Path}",
                    context.Request.Method, context.Request.Path);

            context.Response.ContentType = "application/json";

            context.Response.StatusCode = ex switch
            {
                NotFoundException  => StatusCodes.Status404NotFound,
                BusinessException  => StatusCodes.Status400BadRequest,
                DomainException    => StatusCodes.Status409Conflict,
                _                  => StatusCodes.Status500InternalServerError
            };

            var message = ex is not (NotFoundException or BusinessException or DomainException)
                ? "An unexpected error occurred"
                : ex.Message;

            await context.Response.WriteAsJsonAsync(new { message });
        }
    }
}