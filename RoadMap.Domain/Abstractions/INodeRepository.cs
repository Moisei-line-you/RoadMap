using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Domain.Interfaces;

public interface INodeRepository
{
    Task<Node?> GetFullInfoAsync(int id);
    
    void AddDependency(NodeDependency dependency);
    void RemoveDependency(NodeDependency dependency);
    void AddResourceLink(NodeResource nodeResource);
    Task<Node?> GetAsync(int id);
    Task<IEnumerable<Node>> GetAllAsync();
    public Task<List<Node>> GetByIdsWithDependenciesAsync(List<int> ids);
    public Task<List<Node>> GetAllWithDependenciesAsync();
}