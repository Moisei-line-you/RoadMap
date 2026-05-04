using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Domain.Interfaces;

public interface IResourceRepository
{
    Task<Resource?> GetAsync(int id);
    Task<IEnumerable<Resource>> GetAllAsync();
    Task<bool> ExistsAsync(int id);
}