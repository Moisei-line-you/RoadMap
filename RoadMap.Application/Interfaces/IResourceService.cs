using RoadMap.Application.DTOs.Roadmaps;

namespace RoadMap.Application.Interfaces;

public interface IResourceService
{
    Task<int> CreateAsync(CreateResourceRequest request);

    Task<ResourceDto> GetByIdAsync(int id);

    Task<IEnumerable<ResourceDto>> GetAllAsync();

    Task DeleteAsync(int id);
}