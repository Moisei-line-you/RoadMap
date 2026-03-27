namespace RoadMap.Application.DTO.Nodes;

public class NodeUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Order { get; set; }
}