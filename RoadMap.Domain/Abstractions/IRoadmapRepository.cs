using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Domain.Interfaces;

public interface IRoadmapRepository
{
    // 1. Получить все карты с возможностью поиска
    Task<IEnumerable<Roadmap>> GetAllAsync(string? searchTerm = null);

    // 2. Получить конкретную карту со всеми вложенными данными (Nodes, Resources)
    Task<Roadmap?> GetByIdAsync(Guid id);

    // 3. Получить карты конкретного автора
    Task<IEnumerable<Roadmap>> GetByAuthorIdAsync(Guid userId);

    // 4. Добавить новую карту в базу
    Task AddAsync(Roadmap roadmap);

    // 5. Обновить данные существующей карты
    void Update(Roadmap roadmap);

    // 6. Удалить карту
    void Delete(Roadmap roadmap);

    // 7. Сохранить все изменения в БД (Unit of Work)
    Task<bool> SaveChangesAsync();

    // 8. Проверка существования и владения
    Task<bool> ExistsAsync(Guid id);
    Task<bool> IsAuthorAsync(Guid roadmapId, Guid userId);
}