using RoadMap.Domain.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";

            switch (ex)
            {
                case NotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;
                case BusinessException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case DomainException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            var response = new { message = ex.Message };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}