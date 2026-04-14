using MediatR;
using RoadMap.Application.DTOs.Roadmaps;
using RoadMap.Domain.Exceptions;
using RoadMap.Domain.Interfaces;

namespace RoadMap.Application.Features.Roadmaps.Queries;

public record GetRoadmapQuery(int Id) : IRequest<RoadmapDto>;

public class GetRoadmapHandler : IRequestHandler<GetRoadmapQuery, RoadmapDto>
{
    private readonly IRepository _repository;

    public GetRoadmapHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<RoadmapDto> Handle(GetRoadmapQuery request, CancellationToken cancellationToken)
    {
        var roadmap = await _repository.Roadmaps.GetWithNodesAsync(request.Id);

        if (roadmap == null)
            throw new NotFoundException("Roadmap", request.Id);

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
}
