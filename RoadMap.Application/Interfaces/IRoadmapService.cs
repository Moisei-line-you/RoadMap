using RoadMap.Application.DTOs.Nodes;
using RoadMap.Application.DTOs.Roadmaps;
using RoadMap.Application.Services;
using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Application.Interfaces;

public interface IRoadmapService
{
    Task<RoadmapDto> GetRoadmapAsync(int id);
    public Task AddNodeToRoadmapAsync(int roadmapId, int nodeId, double x, double y);
    Task<IEnumerable<NodeSummaryDto>> GetAvailableNodesAsync(int roadmapId, List<int> completedNodeIds);
    public Task<Roadmap> CreateRoadmap(RoadmapService.CreateRoadmapRequest request);
}