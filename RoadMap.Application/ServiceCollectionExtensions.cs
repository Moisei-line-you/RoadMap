using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RoadMap.Application.Common.Behaviors;
using RoadMap.Application.Features.Auth.Commands.Register;
using RoadMap.Application.Interfaces;
using RoadMap.Application.Services;
using RoadMap.Domain.Interfaces;
using RoadMap.Domain.Services;

namespace RoadMap.Application; 

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        
        services.AddCors(options =>
        {
            options.AddPolicy("FrontendPolicy", policy =>
            {
                policy
                    .WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IDependencyGraphService, DependencyGraphService>();
        
        services.AddValidatorsFromAssembly(typeof(RegisterCommand).Assembly);

        services.AddMediatR(cfg => 
        {
            cfg.RegisterServicesFromAssemblies(typeof(RegisterCommand).Assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return services;
    }
}