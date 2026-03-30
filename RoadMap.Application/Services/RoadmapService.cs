using RoadMap.Application.DTOs.Nodes;
using RoadMap.Application.DTOs.Roadmaps;
using RoadMap.Application.Interfaces;
using RoadMap.Domain.Exceptions;
using RoadMap.Domain.Interfaces;
using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Application.Services;

public class RoadmapService : IRoadmapService
{
    private readonly IRepository _repository;

    public RoadmapService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<RoadmapDto> GetRoadmapAsync(int id)
    {
        var roadmap = await _repository.Roadmaps.GetWithNodesAsync(id)
                      ?? throw new NotFoundException("Roadmap", id);

        return new RoadmapDto(
            roadmap.Id,
            roadmap.Title,
            roadmap.Description,
            roadmap.Nodes.Select(rn => new RoadmapNodeDto(
                rn.NodeId,
                rn.Node?.Title ?? string.Empty,
                rn.PositionX,
                rn.PositionY
            )).ToList()
        );
    }

    public async Task AddNodeToRoadmapAsync(int roadmapId, int nodeId, double x, double y)
    {
        var roadmap = await _repository.Roadmaps.GetWithNodesAsync(roadmapId)
                      ?? throw new NotFoundException("Roadmap", roadmapId);

        roadmap.AddNode(nodeId, x, y);

        await _repository.SaveChangesAsync();
    }

    public async Task<IEnumerable<NodeSummaryDto>> GetAvailableNodesAsync(
        int roadmapId, 
        List<int> completedNodeIds)
    {
        var roadmap = await _repository.Roadmaps.GetWithNodesAsync(roadmapId)
                      ?? throw new NotFoundException("Roadmap", roadmapId);

        var nodeIds = roadmap.Nodes.Select(rn => rn.NodeId).ToList();
        var nodes = await _repository.Nodes.GetByIdsWithDependenciesAsync(nodeIds);
        var completed = completedNodeIds.ToHashSet();

        return nodes
            .Where(n => n.IsAvailable(completed))
            .Select(n => new NodeSummaryDto(
                n.Id,
                n.Title,
                n.Description,
                n.Difficulty,
                n.IsOptional,
                n.DependsOn.Select(d => d.ToNodeId).ToList()
            ));
    }

    public async Task<Roadmap> CreateRoadmap(CreateRoadmapRequest request)
    {
        var roadmap = new Roadmap
        {
            Title = request.Title,
            Description = request.Description
        };

        await _repository.AddAsync(roadmap);
        await _repository.SaveChangesAsync();

        return roadmap;
    }
    
    public record CreateRoadmapRequest(string Title, string Description);
}