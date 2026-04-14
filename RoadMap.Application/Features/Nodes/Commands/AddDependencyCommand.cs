using MediatR;
using RoadMap.Application.DTOs.Nodes;
using RoadMap.Domain.Exceptions;
using RoadMap.Domain.Interfaces;

namespace RoadMap.Application.Features.Nodes.Commands;

public record AddDependencyCommand(AddDependencyRequest Request) : IRequest<Unit>;

public class AddDependencyHandler : IRequestHandler<AddDependencyCommand, Unit>
{
    private readonly IRepository _repository;
    private readonly IDependencyGraphService _graphService;

    public AddDependencyHandler(IRepository repository, IDependencyGraphService graphService)
    {
        _repository = repository;
        _graphService = graphService;
    }

    public async Task<Unit> Handle(AddDependencyCommand request, CancellationToken cancellationToken)
    {
        var fromNode = await _repository.Nodes.GetFullInfoAsync(request.Request.FromNodeId);

        if (fromNode == null)
            throw new NotFoundException("Node", request.Request.FromNodeId);

        var toNode = await _repository.Nodes.GetAsync(request.Request.ToNodeId);

        if (toNode == null)
            throw new NotFoundException("Node", request.Request.ToNodeId);

        var allNodes = await _repository.Nodes.GetAllWithDependenciesAsync();

        if (_graphService.HasCycle(allNodes, request.Request.FromNodeId, request.Request.ToNodeId))
            throw new BusinessException("Dependency creates a cycle");

        var dependency = fromNode.AddDependency(toNode, request.Request.Type);

        _repository.Nodes.AddDependency(dependency);

        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}