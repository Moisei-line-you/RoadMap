namespace RoadMap.Domain.Models.Roadmaps;

public class Resource
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty; 
    public string Url { get; set; } = string.Empty;
    public ResourceType Type { get; set; } // Video, Article, Book, etc.

    public Guid NodeId { get; set; }
    public Node Node { get; set; } = null!;
}

public enum ResourceType
{
    Video,
    Article,
    Book,
    Documentation,
    Exercise
}