using Microsoft.EntityFrameworkCore;
using RoadMap.Application.DTO.Roadmaps;
using RoadMap.Application.Interfaces;
using RoadMap.Data;
using RoadMap.Domain.Interfaces;
using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Application.Services;

public class RoadmapService : IRoadmapService
{
    private readonly IRoadmapRepository _repository;

    public RoadmapService(IRoadmapRepository repository)
    {
        _repository = repository;
    }

    // 1. Получение всех карт
    public async Task<IEnumerable<RoadmapListDto>> GetAllRoadmapsAsync(string? searchTerm = null)
    {
        var roadmaps = await _repository.GetAllAsync(searchTerm);

        return roadmaps.Select(r => new RoadmapListDto
        {
            Id = r.Id,
            Title = r.Title,
            Description = r.Description,
            CreatedAt = r.CreatedAt
        });
    }

    // 2. Детальная карта
    public async Task<RoadmapDetailDto?> GetRoadmapByIdAsync(Guid id)
    {
        var roadmap = await _repository.GetByIdAsync(id);
        if (roadmap == null) return null;

        return new RoadmapDetailDto
        {
            Id = roadmap.Id,
            Title = roadmap.Title,
            Description = roadmap.Description,
            AuthorId = roadmap.AuthorId,
            Nodes = roadmap.Nodes.Select(n => new NodeDto 
            { 
                Id = n.Id, 
                Title = n.Title 
            }).ToList()
        };
    }

    // 3. Карты пользователя
    public async Task<IEnumerable<RoadmapListDto>> GetRoadmapsByUserIdAsync(Guid userId)
    {
        var roadmaps = await _repository.GetByAuthorIdAsync(userId);
        return roadmaps.Select(r => new RoadmapListDto { Id = r.Id, Title = r.Title });
    }

    // 4. Создание
    public async Task<RoadmapListDto> CreateRoadmapAsync(RoadmapCreateDto dto, Guid userId)
    {
        var roadmap = new Roadmap
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            AuthorId = userId,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(roadmap);
        await _repository.SaveChangesAsync();

        return new RoadmapListDto { Id = roadmap.Id, Title = roadmap.Title };
    }

    // 5. Обновление
    public async Task<bool> UpdateRoadmapAsync(Guid id, RoadmapUpdateDto dto, Guid userId)
    {
        var roadmap = await _repository.GetByIdAsync(id);

        if (roadmap == null || roadmap.AuthorId != userId)
            return false;

        roadmap.Title = dto.Title ?? roadmap.Title;
        roadmap.Description = dto.Description ?? roadmap.Description;

        _repository.Update(roadmap);
        return await _repository.SaveChangesAsync();
    }

    // 6. Удаление
    public async Task<bool> DeleteRoadmapAsync(Guid id, Guid userId)
    {
        var roadmap = await _repository.GetByIdAsync(id);

        if (roadmap == null || roadmap.AuthorId != userId)
            return false;

        _repository.Delete(roadmap);
        return await _repository.SaveChangesAsync();
    }

    // 7. Проверка владения
    public async Task<bool> IsOwnerAsync(Guid roadmapId, Guid userId)
    {
        return await _repository.IsAuthorAsync(roadmapId, userId);
    }
}