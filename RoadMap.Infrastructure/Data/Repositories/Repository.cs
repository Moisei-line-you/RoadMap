using RoadMap.Data;
using RoadMap.Domain.Interfaces;

namespace RoadMap.Infrastucture.Data.Repositories;

public class Repository(
    AppDbContext context, 
    IRoadmapRepository roadmaps,
    INodeRepository nodes,
    IUserRepository users)
    : IRepository
{
    public IRoadmapRepository Roadmaps { get; } = roadmaps;
    public INodeRepository Nodes { get; } = nodes;
    public IUserRepository Users { get; } = users;

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

    public async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class =>
        await context.Set<TEntity>().AddAsync(entity);

    public void Delete<TEntity>(TEntity entity) where TEntity : class
        => context.Set<TEntity>().Remove(entity);
}