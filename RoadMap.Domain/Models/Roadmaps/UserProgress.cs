namespace RoadMap.Domain.Models.Roadmaps;

public class UserProgress
{
    public Guid Id { get; set; }

    // Кто учится (Связь с пользователем Моисея)
    public Guid UserId { get; set; }

    // Какой конкретно навык (Node) изучается
    public Guid NodeId { get; set; }
    public Node Node { get; set; } = null!;

    // Статус прохождения
    public ProgressStatus Status { get; set; } = ProgressStatus.NotStarted;

    // Когда начали и когда закончили
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    // Заметки пользователя по этому конкретному навыку
    public string? UserNotes { get; set; } 
}

public enum ProgressStatus
{
    NotStarted,
    InProgress,
    Completed, 
    Skipped  
}