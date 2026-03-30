using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Domain.Interfaces;

public interface IRoadmapRepository
{
    Task<Roadmap?> GetWithNodesAsync(int id);
    Task<RoadmapNode?> GetRoadmapNodeAsync(int roadmapId, int nodeId);
    Task<Roadmap?> GetAsync(int id);
    Task<IEnumerable<Roadmap>> GetAllAsync();
}