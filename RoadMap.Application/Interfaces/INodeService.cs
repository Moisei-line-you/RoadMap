using RoadMap.Domain.Enums;
using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Application.Interfaces;

public interface INodeService
{
    Task<Node> GetFullNodeAsync(int id);
    Task<int> CreateNodeAsync(string title, string description, NodeType type, int difficulty, bool isOptional);
    Task AddDependencyAsync(int fromNodeId, int toNodeId, DependencyType type);
    Task AddResourceAsync(int nodeId, int resourceId);
}