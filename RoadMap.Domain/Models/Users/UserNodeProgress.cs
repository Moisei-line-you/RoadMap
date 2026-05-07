using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Models.Users;

public class UserNodeProgress
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public int NodeId { get; set; }
    public Node? Node { get; set; }

    public int RoadmapId { get; set; }
    public Roadmap? Roadmap { get; set; }

    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
}
 