using MediatR;
using RoadMap.Domain.Exceptions;
using RoadMap.Domain.Interfaces;
using RoadMap.Models.Users;

namespace RoadMap.Application.Features.Progress.Commands;

public record MarkNodeCompleteCommand(
    int UserId,
    int NodeId,
    int RoadmapId
) : IRequest<Unit>;

public class MarkNodeCompleteHandler : IRequestHandler<MarkNodeCompleteCommand, Unit>
{
    private readonly IRepository _repository;

    public MarkNodeCompleteHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(
        MarkNodeCompleteCommand request,
        CancellationToken cancellationToken)
    {
        var roadmap = await _repository.Roadmaps.GetAsync(request.RoadmapId);

        if (roadmap == null)
            throw new NotFoundException("Roadmap", request.RoadmapId);

        var roadmapNode = await _repository.Roadmaps.GetRoadmapNodeAsync(
            request.RoadmapId,
            request.NodeId);

        if (roadmapNode == null)
            throw new BusinessException("Node does not belong to this roadmap");

        var isCompleted = await _repository.Progress.IsNodeCompletedAsync(
            request.UserId,
            request.NodeId,
            request.RoadmapId);

        if (isCompleted)
            throw new BusinessException("Node is already completed");

        var progress = new UserNodeProgress
        {
            UserId = request.UserId,
            NodeId = request.NodeId,
            RoadmapId = request.RoadmapId,
            CompletedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(progress);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}