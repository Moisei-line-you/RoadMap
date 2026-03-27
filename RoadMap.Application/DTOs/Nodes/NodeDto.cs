using RoadMap.Application.DTO.Nodes;

public class NodeDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Order { get; set; }
    
    public string Status { get; set; } = "NotStarted";
    
    public List<ResourceDto> Resources { get; set; } = new();
    
    public List<NodeDto> Children { get; set; } = new();
}