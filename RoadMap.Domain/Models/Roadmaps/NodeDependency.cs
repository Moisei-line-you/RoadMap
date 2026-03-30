using RoadMap.Domain.Enums;

namespace RoadMap.Domain.Models.Roadmaps;

public class NodeDependency
{
    public int FromNodeId { get; set; }
    public Node FromNode { get; set; }
    public int ToNodeId { get; set; }
    public Node ToNode { get; set; }
    public DependencyType Type { get; set; }
}