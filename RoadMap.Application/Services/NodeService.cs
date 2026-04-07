using RoadMap.Application.DTOs.Nodes;
using RoadMap.Application.Interfaces;
using RoadMap.Domain.Exceptions;
using RoadMap.Domain.Interfaces;
using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Application.Services;

public class NodeService : INodeService
{
    private readonly IRepository _repository;
    private readonly IDependencyGraphService _graphService;

    public NodeService(IRepository repository, IDependencyGraphService graphService)
    {
        _repository = repository;
        _graphService = graphService;
    }

    public async Task<NodeDto> GetFullNodeAsync(int id)
    {
        var node = await _repository.Nodes.GetFullInfoAsync(id)
                   ?? throw new NotFoundException("Node", id);

        return new NodeDto(
            node.Id,
            node.Title,
            node.Description
        );
    }

    public async Task AddDependencyAsync(AddDependencyRequest request)
    {
        var fromNode = await _repository.Nodes.GetFullInfoAsync(request.FromNodeId)
                       ?? throw new NotFoundException("Node", request.FromNodeId);

        var toNode = await _repository.Nodes.GetAsync(request.ToNodeId)
                     ?? throw new NotFoundException("Node", request.ToNodeId);

        var nodes = await _repository.Nodes.GetAllWithDependenciesAsync();
        if (_graphService.HasCycle(nodes, request.FromNodeId, request.ToNodeId))
            throw new BusinessException("Dependency creates a cycle");

        var dependency = fromNode.AddDependency(toNode, request.Type);
        _repository.Nodes.AddDependency(dependency);

        await _repository.SaveChangesAsync();
    }

    public async Task AddResourceAsync(AddResourceRequest request)
    {
        var node = await _repository.Nodes.GetFullInfoAsync(request.NodeId)
                   ?? throw new NotFoundException("Node", request.NodeId);

        node.AddResource(request.ResourceId);

        await _repository.SaveChangesAsync();
    }
    
    public async Task<int> CreateNodeAsync(CreateNodeRequest request)
    {
        var node = new Node
        {
            Title = request.Title,
            Description = request.Description,
            Type = request.Type,
            Difficulty = request.Difficulty,
            IsOptional = request.IsOptional
        };

        await _repository.AddAsync(node);
        await _repository.SaveChangesAsync();

        return node.Id;
    }
}