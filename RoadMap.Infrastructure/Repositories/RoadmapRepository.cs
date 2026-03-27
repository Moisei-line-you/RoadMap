using Microsoft.EntityFrameworkCore;
using RoadMap.Data;
using RoadMap.Domain.Interfaces;
using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Infrastructure.Repositories;

public class RoadmapRepository : IRoadmapRepository
{
    private readonly AppDbContext _context;

    public RoadmapRepository(AppDbContext context)
    {
        _context = context;
    }
    
    // 1. Получить все (с поиском)
    public async Task<IEnumerable<Roadmap>> GetAllAsync(string? searchTerm = null)
    {
        var query = _context.Roadmaps.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(r => 
                r.Title.Contains(searchTerm) || 
                r.Description.Contains(searchTerm));
        }

        return await query.ToListAsync();
    }

    // 2. Детальная карта (загружаем всё дерево узлов и ресурсов)
    public async Task<Roadmap?> GetByIdAsync(Guid id)
    {
        return await _context.Roadmaps
            .Include(r => r.Nodes)
                .ThenInclude(n => n.Resources)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    // 3. Карты конкретного автора
    public async Task<IEnumerable<Roadmap>> GetByAuthorIdAsync(Guid userId)
    {
        return await _context.Roadmaps
            .Where(r => r.AuthorId == userId)
            .ToListAsync();
    }

    // 4. Добавление (только в память контекста)
    public async Task AddAsync(Roadmap roadmap)
    {
        await _context.Roadmaps.AddAsync(roadmap);
    }

    // 5. Обновление (EF сам отследит изменения, если объект загружен)
    public void Update(Roadmap roadmap)
    {
        _context.Roadmaps.Update(roadmap);
    }

    // 6. Удаление
    public void Delete(Roadmap roadmap)
    {
        _context.Roadmaps.Remove(roadmap);
    }

    // 7. Cохранение в БД
    public async Task<bool> SaveChangesAsync()
    {
        // Метод вернет true, если в базе изменилась хотя бы 1 строка
        return await _context.SaveChangesAsync() > 0;
    }

    // 8. Вспомогательные проверки
    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Roadmaps.AnyAsync(r => r.Id == id);
    }

    public async Task<bool> IsAuthorAsync(Guid roadmapId, Guid userId)
    {
        return await _context.Roadmaps
            .AnyAsync(r => r.Id == roadmapId && r.AuthorId == userId);
    }
}