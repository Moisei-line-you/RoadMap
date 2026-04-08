using MediatR;
using RoadMap.Application.Common.Results;
using RoadMap.Application.DTOs.Roadmaps;
using RoadMap.Application.Interfaces;

namespace RoadMap.Application.Features.Roadmaps.Commands;

public record AssignNodeToRoadmapCommand(AddNodeToRoadmapRequest Request) : IRequest<Result<Unit>>;

public class AssignNodeToRoadmapHandler : IRequestHandler<AssignNodeToRoadmapCommand, Result<Unit>>
{
    private readonly IRoadmapService _roadmapService;

    public AssignNodeToRoadmapHandler(IRoadmapService roadmapService)
    {
        _roadmapService = roadmapService;
    }

    public async Task<Result<Unit>> Handle(AssignNodeToRoadmapCommand request, CancellationToken cancellationToken)
    {
        await _roadmapService.AddNodeToRoadmapAsync(request.Request);
        return Result<Unit>.Success(Unit.Value);
    }
}