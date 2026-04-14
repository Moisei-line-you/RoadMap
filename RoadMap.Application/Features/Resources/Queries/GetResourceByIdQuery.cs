using MediatR;
using RoadMap.Application.DTOs.Resources;
using RoadMap.Domain.Exceptions;
using RoadMap.Domain.Interfaces;

namespace RoadMap.Application.Features.Resources.Queries;

public record GetResourceByIdQuery(int Id) : IRequest<ResourceDto>;

public class GetResourceByIdHandler : IRequestHandler<GetResourceByIdQuery, ResourceDto>
{
    private readonly IRepository _repository;

    public GetResourceByIdHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResourceDto> Handle(GetResourceByIdQuery request, CancellationToken cancellationToken)
    {
        var resource = await _repository.Resources.GetAsync(request.Id);

        if (resource == null)
            throw new NotFoundException("Resource", request.Id);

        return new ResourceDto(
            Id: resource.Id,
            Title: resource.Title,
            Url: resource.Url,
            Type: resource.Type,
            IsFree: resource.IsFree
        );
    }
}
