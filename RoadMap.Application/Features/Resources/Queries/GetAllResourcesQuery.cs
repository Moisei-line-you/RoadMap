using MediatR;
using RoadMap.Application.DTOs.Resources;
using RoadMap.Domain.Interfaces;

namespace RoadMap.Application.Features.Resources.Queries;

public record GetAllResourcesQuery() : IRequest<IEnumerable<ResourceDto>>;

public class GetAllResourcesHandler : IRequestHandler<GetAllResourcesQuery, IEnumerable<ResourceDto>>
{
    private readonly IRepository _repository;

    public GetAllResourcesHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ResourceDto>> Handle(GetAllResourcesQuery request, CancellationToken cancellationToken)
    {
        var resources = await _repository.Resources.GetAllAsync();

        return resources.Select(r => new ResourceDto(
            Id: r.Id,
            Title: r.Title,
            Url: r.Url,
            Type: r.Type,
            IsFree: r.IsFree
        ));
    }
}