using RoadMap.Application.DTOs.Nodes;
using RoadMap.Application.DTOs.Roadmaps;

namespace RoadMap.Application.Interfaces;

public interface IRoadmapService
{
    Task<RoadmapDto> GetRoadmapAsync(int id);
    public Task AddNodeToRoadmapAsync(AddNodeToRoadmapRequest request);
    Task<IEnumerable<NodeSummaryDto>> GetAvailableNodesAsync(GetAvailableNodesRequest request);
    public Task<RoadmapDto> CreateRoadmap(CreateRoadmapRequest request);
}