using RoadMap.Application.Interfaces;
using RoadMap.Domain.Enums;
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

    public async Task<Node> GetFullNodeAsync(int id)
    {
        return await _repository.Nodes.GetFullInfoAsync(id)
               ?? throw new NotFoundException("Node", id);
    }

    public async Task AddDependencyAsync(int fromNodeId, int toNodeId, DependencyType type)
    {
        var fromNode = await _repository.Nodes.GetFullInfoAsync(fromNodeId)
                       ?? throw new NotFoundException("Node", fromNodeId);

        var toNode = await _repository.Nodes.GetAsync(toNodeId)
                     ?? throw new NotFoundException("Node", toNodeId);

        var nodes = await _repository.Nodes.GetAllAsync();
        if (_graphService.HasCycle(nodes, fromNodeId, toNodeId))
            throw new BusinessException("Dependency creates a cycle");

        var dependency = fromNode.AddDependency(toNode, type);
        _repository.Nodes.AddDependency(dependency);

        await _repository.SaveChangesAsync();
    }

    public async Task AddResourceAsync(int nodeId, int resourceId)
    {
        var node = await _repository.Nodes.GetFullInfoAsync(nodeId)
                   ?? throw new NotFoundException("Node", nodeId);

        node.AddResource(resourceId);

        await _repository.SaveChangesAsync();
    }
    
    public async Task<int> CreateNodeAsync(
        string title,
        string description,
        NodeType type,
        int difficulty,
        bool isOptional)
    {
        var node = new Node
        {
            Title = title,
            Description = description,
            Type = type,
            Difficulty = difficulty,
            IsOptional = isOptional
        };

        await _repository.AddAsync(node);
        await _repository.SaveChangesAsync();

        return node.Id;
    }
}