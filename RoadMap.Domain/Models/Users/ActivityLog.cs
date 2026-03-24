namespace RoadMap.Models.Users;

public class ActivityLog
{
    public int Id { get; set; }
    public int UserId { get; set; } // Чье это действие
    public string Action { get; set; } = string.Empty; // Текстовое описание действия
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Когда это произошло
}