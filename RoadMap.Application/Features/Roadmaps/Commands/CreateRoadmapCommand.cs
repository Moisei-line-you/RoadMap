using MediatR;
using RoadMap.Application.Common.Results;
using RoadMap.Application.DTOs.Roadmaps;
using RoadMap.Application.Interfaces;

namespace RoadMap.Application.Features.Roadmaps.Commands;

public record CreateRoadmapCommand(CreateRoadmapRequest Request) : IRequest<Result<RoadmapDto>>;

public class CreateRoadmapHandler : IRequestHandler<CreateRoadmapCommand, Result<RoadmapDto>>
{
    private readonly IRoadmapService _roadmapService;

    public CreateRoadmapHandler(IRoadmapService roadmapService)
    {
        _roadmapService = roadmapService;
    }

    public async Task<Result<RoadmapDto>> Handle(CreateRoadmapCommand request, CancellationToken cancellationToken)
    {
        var roadmapDto = await _roadmapService.CreateRoadmap(request.Request);
        return Result<RoadmapDto>.Success(roadmapDto);
    }
}