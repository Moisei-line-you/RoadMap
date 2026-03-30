namespace RoadMap.Domain.Interfaces;

public interface IRepository
{
    IRoadmapRepository Roadmaps { get; }
    INodeRepository Nodes { get; }
    IUserRepository Users { get; }
    Task<int> SaveChangesAsync();
}