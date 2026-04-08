using MediatR;
using RoadMap.Application.Common.Results;
using RoadMap.Application.DTOs.Resources;
using RoadMap.Domain.Interfaces;

namespace RoadMap.Application.Features.Resources.Queries;

public record GetResourceByIdQuery(int Id) : IRequest<Result<ResourceDto>>;

public class GetResourceByIdHandler : IRequestHandler<GetResourceByIdQuery, Result<ResourceDto>>
{
    private readonly IRepository _repository;

    public GetResourceByIdHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<ResourceDto>> Handle(GetResourceByIdQuery request, CancellationToken cancellationToken)
    {
        var resource = await _repository.Resources.GetAsync(request.Id);
        if (resource == null)
            return Result<ResourceDto>.Failure("Resource not found");

        var resourceDto = new ResourceDto(
            Id: resource.Id,
            Title: resource.Title,
            Url: resource.Url,
            Type: resource.Type,
            IsFree: resource.IsFree
        );

        return Result<ResourceDto>.Success(resourceDto);
    }
}
