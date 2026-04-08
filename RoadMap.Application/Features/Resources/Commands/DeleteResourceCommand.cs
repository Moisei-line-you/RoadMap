using MediatR;
using RoadMap.Application.Common.Results;
using RoadMap.Domain.Interfaces;

namespace RoadMap.Application.Features.Resources.Queries;

public record DeleteResourceCommand(int Id) : IRequest<Result<Unit>>;

public class DeleteResourceHandler : IRequestHandler<DeleteResourceCommand, Result<Unit>>
{
    private readonly IRepository _repository;

    public DeleteResourceHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Unit>> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
    {
        var resource = await _repository.Resources.GetAsync(request.Id);

        if (resource == null)
            return Result<Unit>.Failure("Resource not found");

        await _repository.Resources.DeleteAsync(request.Id);
        await _repository.SaveChangesAsync();

        return Result<Unit>.Success(Unit.Value);
    }
}