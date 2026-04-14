using MediatR;
using RoadMap.Application.DTOs.Nodes;
using RoadMap.Domain.Exceptions;
using RoadMap.Domain.Interfaces;

namespace RoadMap.Application.Features.Roadmaps.Queries;

public record GetAvailableNodesQuery(
    int RoadmapId,
    List<int> CompletedNodeIds
) : IRequest<IEnumerable<NodeSummaryDto>>;

public class GetAvailableNodesHandler 
    : IRequestHandler<GetAvailableNodesQuery, IEnumerable<NodeSummaryDto>>
{
    private readonly IRepository _repository;

    public GetAvailableNodesHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<NodeSummaryDto>> Handle(
        GetAvailableNodesQuery request,
        CancellationToken cancellationToken)
    {
        var roadmap = await _repository.Roadmaps.GetWithNodesAsync(request.RoadmapId);

        if (roadmap == null)
            throw new NotFoundException("Roadmap", request.RoadmapId);

        return roadmap.Nodes
            .Where(n => !request.CompletedNodeIds.Contains(n.NodeId))
            .Select(n => new NodeSummaryDto(
                n.NodeId,
                n.Node.Title,
                n.Node.Description,
                n.Node.Difficulty,
                n.Node.IsOptional,
                n.DependsOn?.Select(d => d.ToNodeId).ToList() ?? new List<int>()
            ));
    }
}