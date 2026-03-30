using RoadMap.Domain.Interfaces;
using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Domain.Services;

public class DependencyGraphService : IDependencyGraphService
{
    public bool HasCycle(IEnumerable<Node> allNodes, int fromNodeId, int toNodeId)
    {
        var graph = allNodes.ToDictionary(
            n => n.Id,
            n => n.DependsOn.Select(d => d.ToNodeId).ToList()
        );
        return HasPath(graph, fromNodeId, toNodeId);
    }

    private bool HasPath(Dictionary<int, List<int>> graph, int current, int target, HashSet<int>? visited = null)
    {
        visited ??= new HashSet<int>();

        if (current == target)
            return true;

        if (!visited.Add(current))
            return false;

        if (!graph.ContainsKey(current))
            return false;

        return graph[current].Any(next => HasPath(graph, next, target, visited));
    }
}