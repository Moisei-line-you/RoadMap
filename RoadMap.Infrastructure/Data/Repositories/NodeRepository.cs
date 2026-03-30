using Microsoft.EntityFrameworkCore;
using RoadMap.Data;
using RoadMap.Domain.Interfaces;
using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Infrastucture.Data.Repositories;

public class NodeRepository(AppDbContext context) : INodeRepository
{
    public async Task<Node?> GetAsync(int id) => await context.Nodes.FirstOrDefaultAsync(n => n.Id == id);

    public async Task<IEnumerable<Node>> GetAllAsync() => await context.Nodes.ToListAsync();

    public async Task<Node?> GetFullInfoAsync(int id)
    {
        return await context.Nodes
            .Include(n => n.Resources)
            .ThenInclude(nr => nr.Resource)
            .Include(n => n.DependsOn)
            .Include(n => n.RequiredFor)
            .FirstOrDefaultAsync(n => n.Id == id);
    }

    // Работа с промежуточными таблицами напрямую
    public void AddDependency(NodeDependency dependency) => context.NodeDependencies.Add(dependency);
    
    public void RemoveDependency(NodeDependency dependency) => context.NodeDependencies.Remove(dependency);

    public void AddResourceLink(NodeResource nodeResource) => context.NodeResources.Add(nodeResource);
}