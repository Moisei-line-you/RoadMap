using RoadMap.Domain.Enums;
using RoadMap.Domain.Exceptions;

namespace RoadMap.Domain.Models.Roadmaps;

public class Node
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public NodeType Type { get; set; }
    public int Difficulty { get; set; }
    public List<RoadmapNode> Roadmaps { get; set; } = [];
    public List<NodeDependency> DependsOn { get; set; } = [];
    public List<NodeDependency> RequiredFor { get; set; } = [];
    public List<NodeResource> Resources { get; set; } = []; 
    public bool IsOptional { get; set; }
    
    public NodeDependency AddDependency(Node target, DependencyType type)
    {
        if (Id == target.Id)
            throw new DomainException("Node cannot depend on itself");

        if (DependsOn.Any(d => d.ToNodeId == target.Id))
            throw new DomainException("Dependency already exists");

        var dependency = new NodeDependency
        {
            FromNodeId = Id,
            ToNodeId = target.Id,
            Type = type
        };
        
        DependsOn.Add(dependency);
        target.RequiredFor.Add(dependency);
        return dependency; 
    }

    public void AddResource(int resourceId)
    {
        if (Resources.Any(r => r.ResourceId == resourceId))
            throw new DomainException("Resource already added");

        Resources.Add(new NodeResource
        {
            NodeId = Id,
            ResourceId = resourceId
        });
    }
    
    public bool IsAvailable(IReadOnlySet<int> completedNodeIds)
    {
        return DependsOn.All(d => completedNodeIds.Contains(d.ToNodeId));
    }
}