using RoadMap.Domain.Enums;

namespace RoadMap.Domain.Models.Roadmaps;

public class Resource
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public ResourceType Type { get; set; }
    public bool IsFree { get; set; }
}