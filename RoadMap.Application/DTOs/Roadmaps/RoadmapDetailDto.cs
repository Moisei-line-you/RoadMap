namespace RoadMap.Application.DTO.Roadmaps;

public class RoadmapDetailDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid AuthorId { get; set; }
    public List<NodeDto> Nodes { get; set; } = new();
}