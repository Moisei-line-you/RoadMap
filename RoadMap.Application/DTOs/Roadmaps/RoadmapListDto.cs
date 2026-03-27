namespace RoadMap.Application.DTO.Roadmaps;

public class RoadmapListDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public int NodesCount { get; set; }
    public DateTime CreatedAt { get; set; }
}