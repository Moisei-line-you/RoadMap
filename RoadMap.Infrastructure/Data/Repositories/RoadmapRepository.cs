using Microsoft.EntityFrameworkCore;
using RoadMap.Data;
using RoadMap.Domain.Interfaces;
using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Infrastucture.Data.Repositories;

public class RoadmapRepository(AppDbContext context) : IRoadmapRepository
{
    public async Task<Roadmap?> GetAsync(int id) => await context.Roadmaps.FirstOrDefaultAsync(r => r.Id == id);

    public async Task<IEnumerable<Roadmap>> GetAllAsync() => await context.Roadmaps.ToListAsync();

    public async Task<Roadmap?> GetWithNodesAsync(int id)
    {
        return await context.Roadmaps
            .Include(r => r.Nodes)
            .ThenInclude(rn => rn.Node)
            .AsSplitQuery()            
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<RoadmapNode?> GetRoadmapNodeAsync(int roadmapId, int nodeId)
    {
        return await context.RoadmapNodes
            .FirstOrDefaultAsync(rn => rn.RoadmapId == roadmapId && rn.NodeId == nodeId);
    }
}