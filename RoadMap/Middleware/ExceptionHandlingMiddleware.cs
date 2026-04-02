using FluentValidation;
using System.Text.Json;

namespace RoadMap.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); 
        }
        catch (ValidationException ex) 
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var errors = ex.Errors.Select(e => new 
            { 
                Field = e.PropertyName, 
                Message = e.ErrorMessage 
            });

            var response = new 
            { 
                title = "Ошибка валидации", 
                status = 400,
                errors = errors 
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (Exception ex) 
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = "Внутренняя ошибка сервера" }));
        }
    }
}