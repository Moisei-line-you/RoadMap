using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RoadMap.Application.Common.Behaviors;
using RoadMap.Application.Interfaces;
using RoadMap.Application.Services;
using RoadMap.Domain.Interfaces;
using RoadMap.Domain.Services;

namespace RoadMap.Application; 

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<INodeService, NodeService>();
        services.AddScoped<IRoadmapService, RoadmapService>();
        services.AddScoped<IResourceService, ResourceService>();
        services.AddScoped<IDependencyGraphService, DependencyGraphService>();
        
        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);

        services.AddMediatR(cfg => 
        {
            cfg.RegisterServicesFromAssemblies(typeof(ServiceCollectionExtensions).Assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return services;
    }
}