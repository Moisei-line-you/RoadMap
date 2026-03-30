namespace RoadMap.Domain.Models.Roadmaps;

public class Roadmap
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<RoadmapNode> Nodes { get; set; }
}