using MediatR;
using RoadMap.Application.Common.Results;
using RoadMap.Application.DTOs.Nodes;
using RoadMap.Application.Interfaces;

namespace RoadMap.Application.Features.Roadmaps.Queries;

public record GetAvailableNodesQuery(
    int RoadmapId,
    List<int> CompletedNodeIds
) : IRequest<Result<IEnumerable<NodeSummaryDto>>>;

public class GetAvailableNodesHandler 
    : IRequestHandler<GetAvailableNodesQuery, Result<IEnumerable<NodeSummaryDto>>>
{
    private readonly IRoadmapService _roadmapService;

    public GetAvailableNodesHandler(IRoadmapService roadmapService)
    {
        _roadmapService = roadmapService;
    }

    public async Task<Result<IEnumerable<NodeSummaryDto>>> Handle(
        GetAvailableNodesQuery request,
        CancellationToken cancellationToken)
    {
        var nodes = await _roadmapService.GetAvailableNodesAsync(
            new GetAvailableNodesRequest(request.RoadmapId, request.CompletedNodeIds));

        return Result<IEnumerable<NodeSummaryDto>>.Success(nodes);
    }
}