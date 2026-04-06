using RoadMap.Application.DTOs.Nodes;

namespace RoadMap.Application.Interfaces;

public interface INodeService
{
    Task<NodeDto> GetFullNodeAsync(int id);
    Task<int> CreateNodeAsync(CreateNodeRequest request);
    Task AddDependencyAsync(AddDependencyRequest request);
    Task AddResourceAsync(AddResourceRequest request);
}