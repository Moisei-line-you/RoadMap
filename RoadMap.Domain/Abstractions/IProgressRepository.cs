using RoadMap.Models.Users;

namespace RoadMap.Domain.Interfaces;

public interface IProgressRepository
{
    Task<List<int>> GetCompletedNodeIdsAsync(int userId, int roadmapId);
    
    Task<bool> IsNodeCompletedAsync(int userId, int nodeId, int roadmapId);
    
    Task<List<UserNodeProgress>> GetAsync(int userId, int roadmapId);
    
    Task<UserNodeProgress?> GetAsync(int userId, int nodeId, int roadmapId);
}