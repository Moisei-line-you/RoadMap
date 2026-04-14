using MediatR;
using RoadMap.Application.DTOs.Nodes;
using RoadMap.Domain.Exceptions;
using RoadMap.Domain.Interfaces;

namespace RoadMap.Application.Features.Nodes.Commands;

public record AddResourceCommand(AddResourceRequest Request) : IRequest<Unit>;

public class AddResourceHandler : IRequestHandler<AddResourceCommand, Unit>
{
    private readonly IRepository _repository;

    public AddResourceHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(AddResourceCommand request, CancellationToken cancellationToken)
    {
        var node = await _repository.Nodes.GetFullInfoAsync(request.Request.NodeId);

        if (node == null)
            throw new NotFoundException("Node", request.Request.NodeId);

        node.AddResource(request.Request.ResourceId);

        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}