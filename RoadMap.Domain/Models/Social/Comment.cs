namespace RoadMap.Models.Social;

public class Comment
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public int UserId { get; set; }
    public int NodeId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}