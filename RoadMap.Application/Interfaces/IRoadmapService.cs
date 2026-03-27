using RoadMap.Application.DTO.Roadmaps;

namespace RoadMap.Application.Interfaces;

public interface IRoadmapService
{
    // Получение списка всех карт (Main menu)
    Task<IEnumerable<RoadmapListDto>> GetAllRoadmapsAsync(string? searchTerm = null);

    // Получение одной конкретной карты со всеми ее узлами (Nodes)
    Task<RoadmapDetailDto?> GetRoadmapByIdAsync(Guid id);

    // Получение всех карт, созданных конкретным пользователем
    Task<IEnumerable<RoadmapListDto>> GetRoadmapsByUserIdAsync(Guid userId);

    // Создание новой карты
    Task<RoadmapListDto> CreateRoadmapAsync(RoadmapCreateDto dto, Guid userId);

    // Обновление основной информации (название, описание)
    Task<bool> UpdateRoadmapAsync(Guid id, RoadmapUpdateDto dto, Guid userId);

    // Удаление карты
    Task<bool> DeleteRoadmapAsync(Guid id, Guid userId);

    // Метод для проверки, является ли пользователь владельцем (понадобится для прав доступа)
    Task<bool> IsOwnerAsync(Guid roadmapId, Guid userId);
}