namespace RoadMap.Domain.Models.Roadmaps;

public class NodeResource
{
    public int NodeId { get; set; }
    public Node Node { get; set; }
    public int ResourceId { get; set; }
    public Resource Resource { get; set; }
}