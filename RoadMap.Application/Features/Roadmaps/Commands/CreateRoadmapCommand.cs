using MediatR;
using RoadMap.Application.DTOs.Roadmaps;
using RoadMap.Domain.Interfaces;
using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Application.Features.Roadmaps.Commands;

public record CreateRoadmapCommand(CreateRoadmapRequest Request) : IRequest<RoadmapDto>;

public class CreateRoadmapHandler : IRequestHandler<CreateRoadmapCommand, RoadmapDto>
{
    private readonly IRepository _repository;

    public CreateRoadmapHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<RoadmapDto> Handle(CreateRoadmapCommand request, CancellationToken cancellationToken)
    {
        var roadmap = new Roadmap
        {
            Title = request.Request.Title,
            Description = request.Request.Description,
        };

        await _repository.AddAsync(roadmap);
        await _repository.SaveChangesAsync();

        return new RoadmapDto(
            roadmap.Id,
            roadmap.Title,
            roadmap.Description,
            new List<RoadmapNodeDto>()
        );
    }
}