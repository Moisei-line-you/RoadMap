using Microsoft.EntityFrameworkCore;
using RoadMap.Data;
using RoadMap.Models.Users;

namespace RoadMap.Infrastucture.Data.Repositories;

public class ProgressRepository(AppDbContext context) : IProgressRepository
{
    public async Task<List<int>> GetCompletedNodeIdsAsync(int userId, int roadmapId)
    {
        return await context.UserNodeProgresses
            .Where(p => p.UserId == userId &&
                        p.RoadmapId == roadmapId)
            .Select(p => p.NodeId)
            .ToListAsync();
    }

    public async Task<bool> IsNodeCompletedAsync(int userId, int nodeId, int roadmapId)
    {
        return await context.UserNodeProgresses
            .AnyAsync(p => p.UserId == userId &&
                           p.NodeId == nodeId &&
                           p.RoadmapId == roadmapId);
    }
    
    public async Task<List<UserNodeProgress>> GetProgressAsync(int userId, int roadmapId)
    {
        return await context.UserNodeProgresses
            .Where(p => p.UserId == userId &&
                        p.RoadmapId == roadmapId)
            .ToListAsync();
    }
    public async Task<UserNodeProgress?> GetAsyncProgress(int userId, int nodeId, int roadmapId)
    {
        return await context.UserNodeProgresses
            .FirstOrDefaultAsync(p =>
                p.UserId == userId &&
                p.NodeId == nodeId &&
                p.RoadmapId == roadmapId);
    }
}