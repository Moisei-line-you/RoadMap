using Microsoft.EntityFrameworkCore;
using RoadMap.Data;
using RoadMap.Domain.Interfaces;
using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Infrastucture.Data.Repositories;

public class ResourceRepository(AppDbContext context) : IResourceRepository
{
    public async Task<Resource?> GetAsync(int id)
        => await context.Resources.FirstOrDefaultAsync(r => r.Id == id);

    public async Task<IEnumerable<Resource>> GetAllAsync()
    {
        return await context.Resources.ToListAsync();
    }
    
    public async Task<bool> ExistsAsync(int id)
        => await context.Resources.AnyAsync(r => r.Id == id);
}