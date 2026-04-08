using MediatR;
using RoadMap.Application.Common.Results;
using RoadMap.Application.DTOs.Resources;
using RoadMap.Domain.Interfaces;

namespace RoadMap.Application.Features.Resources.Queries;

public record GetAllResourcesQuery() : IRequest<Result<IEnumerable<ResourceDto>>>;

public class GetAllResourcesHandler : IRequestHandler<GetAllResourcesQuery, Result<IEnumerable<ResourceDto>>>
{
    private readonly IRepository _repository;

    public GetAllResourcesHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<ResourceDto>>> Handle(GetAllResourcesQuery request, CancellationToken cancellationToken)
    {
        var resources = await _repository.Resources.GetAllAsync();
        
        var resourcesDto = resources.Select(r => new ResourceDto(
            Id: r.Id,
            Title: r.Title,
            Url: r.Url,
            Type: r.Type,
            IsFree: r.IsFree
        ));

        return Result<IEnumerable<ResourceDto>>.Success(resourcesDto);
    }
}