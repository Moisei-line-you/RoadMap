using MediatR;
using RoadMap.Application.Common.Results;
using RoadMap.Application.DTOs.Resources;
using RoadMap.Domain.Interfaces;
using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Application.Features.Resources.Queries;

public record CreateResourceCommand(CreateResourceRequest Request) : IRequest<Result<int>>;

public class CreateResourceHandler : IRequestHandler<CreateResourceCommand, Result<int>>
{
    private readonly IRepository _repository;

    public CreateResourceHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<int>> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var resource = new Resource
            {
                Title = request.Request.Title,
                Url = request.Request.Url,
                Type = request.Request.Type,
                IsFree = request.Request.IsFree
            };

            await _repository.AddAsync(resource);
            await _repository.SaveChangesAsync();

          
            return Result<int>.Success(resource.Id);
        }
        catch (Exception ex)
        {
            return Result<int>.Failure(ex.Message);
        }
    }
}
