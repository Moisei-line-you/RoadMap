using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Domain.Interfaces;

public interface IDependencyGraphService
{
    bool HasCycle(IEnumerable<Node> allNodes, int fromNodeId, int toNodeId);
}