namespace RoadMap.Domain.Interfaces;

public interface IRepository
{
    IRoadmapRepository Roadmaps { get; }
    INodeRepository Nodes { get; }
    IUserRepository Users { get; }
    Task<int> SaveChangesAsync();
    
    Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class;

    void Delete<TEntity>(TEntity entity)
        where TEntity : class;
}