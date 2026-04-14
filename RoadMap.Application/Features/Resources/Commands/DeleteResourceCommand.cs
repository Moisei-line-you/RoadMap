using MediatR;
using RoadMap.Domain.Exceptions;
using RoadMap.Domain.Interfaces;

namespace RoadMap.Application.Features.Resources.Queries;

public record DeleteResourceCommand(int Id) : IRequest<Unit>;

public class DeleteResourceHandler : IRequestHandler<DeleteResourceCommand, Unit>
{
    private readonly IRepository _repository;

    public DeleteResourceHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
    {
        var resource = await _repository.Resources.GetAsync(request.Id);

        if (resource == null)
            throw new NotFoundException("Resource", request.Id);

        _repository.Delete(resource);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}