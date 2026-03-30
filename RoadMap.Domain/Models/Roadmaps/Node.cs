using RoadMap.Domain.Enums;

namespace RoadMap.Domain.Models.Roadmaps;

public class Node
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public NodeType Type { get; set; }
    public int Difficulty { get; set; }
    public List<RoadmapNode> Roadmaps { get; set; }
    public List<NodeDependency> DependsOn { get; set; }
    public List<NodeDependency> RequiredFor { get; set; }
    public List<NodeResource> Resources { get; set; }
    public bool IsOptional { get; set; }
}