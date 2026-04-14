using MediatR;
using RoadMap.Application.DTOs.Roadmaps;
using RoadMap.Domain.Exceptions;
using RoadMap.Domain.Interfaces;

namespace RoadMap.Application.Features.Roadmaps.Commands;

public record AssignNodeToRoadmapCommand(AddNodeToRoadmapRequest Request) : IRequest<Unit>;

public class AssignNodeToRoadmapHandler : IRequestHandler<AssignNodeToRoadmapCommand, Unit>
{
    private readonly IRepository _repository;

    public AssignNodeToRoadmapHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(AssignNodeToRoadmapCommand request, CancellationToken cancellationToken)
    {
        var roadmap = await _repository.Roadmaps.GetWithNodesAsync(request.Request.RoadmapId);

        if (roadmap == null)
            throw new NotFoundException("Roadmap", request.Request.RoadmapId);

        roadmap.AddNode(
            request.Request.NodeId,
            request.Request.X,
            request.Request.Y
        );

        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}