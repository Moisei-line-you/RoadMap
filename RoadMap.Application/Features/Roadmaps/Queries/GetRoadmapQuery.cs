using MediatR;
using RoadMap.Application.Common.Results;
using RoadMap.Application.DTOs.Roadmaps;
using RoadMap.Application.Interfaces;

namespace RoadMap.Application.Features.Roadmaps.Queries;

public record GetRoadmapQuery(int Id) : IRequest<Result<RoadmapDto>>;

public class GetRoadmapHandler : IRequestHandler<GetRoadmapQuery, Result<RoadmapDto>>
{
    private readonly IRoadmapService _roadmapService;

    public GetRoadmapHandler(IRoadmapService roadmapService)
    {
        _roadmapService = roadmapService;
    }

    public async Task<Result<RoadmapDto>> Handle(GetRoadmapQuery request, CancellationToken cancellationToken)
    {
        var roadmap = await _roadmapService.GetRoadmapAsync(request.Id);
        if (roadmap == null)
            return Result<RoadmapDto>.Failure("Roadmap not found");

        return Result<RoadmapDto>.Success(roadmap);
    }
}
