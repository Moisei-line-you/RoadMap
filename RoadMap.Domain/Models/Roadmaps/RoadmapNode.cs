namespace RoadMap.Domain.Models.Roadmaps;

public class RoadmapNode
{
    public int RoadmapId { get; set; }
    public Roadmap Roadmap { get; set; }
    public int NodeId { get; set; }
    public Node Node { get; set; }
    public double PositionX { get; set; }
    public double PositionY { get; set; }
}