namespace RoadMap.Application.DTO.Nodes;

public class NodeCreateDto
{
    public Guid RoadmapId { get; set; }
    public Guid? ParentNodeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Order { get; set; }
}