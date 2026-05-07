using MediatR;
using RoadMap.Domain.Exceptions;
using RoadMap.Domain.Interfaces;
using RoadMap.Domain.Models.Roadmaps;
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
        var roadmap = await GetRoadmapWithNodesOrThrowAsync(request.RoadmapId);
        ValidateNodeBelongsToRoadmap(roadmap, request.NodeId);
        await EnsureNodeNotCompletedAsync(request.UserId,request.NodeId, request.RoadmapId);
        await SaveProgressAsync(request);
        
        return Unit.Value;
    }

    private async Task<Roadmap> GetRoadmapWithNodesOrThrowAsync(int roadmapId)
    {
        var roadmap = await _repository.Roadmaps.GetWithNodesAsync(roadmapId);
        if (roadmap == null)
            throw new NotFoundException("Roadmap", roadmapId);
        return roadmap;
    }

    private static void ValidateNodeBelongsToRoadmap(Roadmap roadmap, int nodeId)
    {
        if (!roadmap.Nodes.Any(n => n.NodeId == nodeId))
            throw new BusinessException(
                $"Node {nodeId} does not belong to roadmap {roadmap.Id}");
    }

    private async Task EnsureNodeNotCompletedAsync(int userId, int nodeId, int roadmapId)
    {
        var isCompleted = await _repository.Progress.IsNodeCompletedAsync(userId, nodeId, roadmapId);

        if (isCompleted)
            throw new BusinessException("Node is already completed");
    }

    private async Task SaveProgressAsync(MarkNodeCompleteCommand request)
    {
        var progress = new UserNodeProgress
        {
            UserId = request.UserId,
            NodeId = request.NodeId,
            RoadmapId = request.RoadmapId,
        };

        await _repository.AddAsync(progress);
        await _repository.SaveChangesAsync();
    }
}