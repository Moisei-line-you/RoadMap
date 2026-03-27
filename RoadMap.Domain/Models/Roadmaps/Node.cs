namespace RoadMap.Domain.Models.Roadmaps;

public class Node
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Order { get; set; }
    public Guid RoadmapId { get; set; }
    public Roadmap Roadmap { get; set; } = null!;
    public Guid? ParentNodeId { get; set; }
    public Node? ParentNode { get; set; }
    public ICollection<Node> Children { get; set; } = new List<Node>();
    public ICollection<Resource> Resources { get; set; } = new List<Resource>();
}