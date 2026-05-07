using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoadMap.Data;
using RoadMap.Domain.Interfaces;
using RoadMap.Infrastucture.Data.Repositories;

namespace RoadMap.Infrastucture;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IRepository, Repository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<INodeRepository, NodeRepository>();
        services.AddScoped<IRoadmapRepository, RoadmapRepository>();
        services.AddScoped<IResourceRepository, ResourceRepository>();
        services.AddScoped<IProgressRepository, ProgressRepository>();

        return services;
    }
}