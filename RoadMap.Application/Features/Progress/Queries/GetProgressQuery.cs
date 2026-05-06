using MediatR;
using RoadMap.Application.DTOs.Progress;
using RoadMap.Domain.Exceptions;
using RoadMap.Domain.Interfaces;

namespace RoadMap.Application.Features.Progress.Queries;

public record GetProgressQuery(int UserId, int RoadMapId) : IRequest<RoadmapProgressDto>;

public class GetProgressHandler : IRequestHandler<GetProgressQuery, RoadmapProgressDto>
{
    private readonly IRepository _repository;
    
    public GetProgressHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<RoadmapProgressDto> Handle(GetProgressQuery request, CancellationToken cancellationToken)
    {
        var roadmap = await _repository.Roadmaps.GetWithNodesAsync(request.RoadMapId);
        if (roadmap == null)
            throw new NotFoundException(nameof(RoadMap), request.RoadMapId);
      
        var progressRecord = await _repository.Progress.GetAsync(request.UserId, request.RoadMapId);

        var completedNodes = progressRecord
            .Select(p => new CompletedNodeDto(
                NodeId: p.NodeId,
                NodeTitle: p.Node?.Title ?? roadmap.Nodes.FirstOrDefault(n => n.NodeId == p.NodeId)?.Node?.Title ?? "Unknown",
                CompletedAt: p.CompletedAt
            ))
            .ToList();
        
        int totalNodes = roadmap.Nodes.Count;
        int completedCount = completedNodes.Count;
        int percentComplete = totalNodes == 0 ? 0 : (int)Math.Round((double)completedCount / totalNodes * 100);

        return new RoadmapProgressDto(
            RoadmapId: request.RoadMapId,
            TotalNodes: totalNodes,
            CompletedCount: completedCount,
            PercentComplete: percentComplete,
            CompletedNodes: completedNodes);    
    }
}
