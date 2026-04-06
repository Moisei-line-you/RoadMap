using RoadMap.Domain.Exceptions;

namespace RoadMap.Domain.Models.Roadmaps;

public class Roadmap
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<RoadmapNode> Nodes { get; set; } = [];
    
    public void AddNode(int nodeId, double x, double y)
    {
        if (Nodes.Any(n => n.NodeId == nodeId))
            throw new DomainException($"Node with id {nodeId} already exists in this roadmap.");

        Nodes.Add(new RoadmapNode
        {
            NodeId = nodeId,
            PositionX = x,
            PositionY = y
        });
    }
}