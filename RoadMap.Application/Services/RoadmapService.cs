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
                rn.PositionX,
                rn.PositionY
            )).ToList()
        );
    }

    public async Task AddNodeToRoadmapAsync(AddNodeToRoadmapRequest request)
    {
        var roadmap = await _repository.Roadmaps.GetWithNodesAsync(request.RoadmapId)
                      ?? throw new NotFoundException("Roadmap", request.RoadmapId);

        roadmap.AddNode(request.NodeId, request.X, request.Y);

        await _repository.SaveChangesAsync();
    }

    public async Task<IEnumerable<NodeSummaryDto>> GetAvailableNodesAsync(GetAvailableNodesRequest request)
    {
        var roadmap = await _repository.Roadmaps.GetWithNodesAsync(request.RoadmapId)
                      ?? throw new NotFoundException("Roadmap", request.RoadmapId);

        var availableNodes = roadmap.Nodes
            .Where(n => !request.CompletedNodeIds.Contains(n.NodeId))
            .Select(n => new NodeSummaryDto(
                n.NodeId,
                n.Node.Title,
                n.Node.Description,
                n.Node.Difficulty,
                n.Node.IsOptional,
                n.DependsOn?.Select(d => d.ToNodeId).ToList() ?? new List<int>()
            ));
        
        return availableNodes;
    }

    public async Task<RoadmapDto> CreateRoadmap(CreateRoadmapRequest request)
    {
        var roadmap = new Roadmap
        {
            Title = request.Title,
            Description = request.Description,
        };

        await _repository.AddAsync(roadmap);
        await _repository.SaveChangesAsync();
        
        return new RoadmapDto(
            roadmap.Id,
            roadmap.Title,
            roadmap.Description,
            roadmap.Nodes.Select(n => new RoadmapNodeDto(
                n.NodeId,
                n.PositionX,
                n.PositionY
            )).ToList()
        );
    }
}