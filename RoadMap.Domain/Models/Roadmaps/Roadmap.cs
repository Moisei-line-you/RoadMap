namespace RoadMap.Domain.Models.Roadmaps;

public class Roadmap
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid AuthorId { get; set; }
    public ICollection<Node> Nodes { get; set; } = new List<Node>();
}