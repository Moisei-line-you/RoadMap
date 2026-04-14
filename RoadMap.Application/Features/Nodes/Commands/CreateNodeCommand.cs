using MediatR;
using RoadMap.Application.DTOs.Nodes;
using RoadMap.Domain.Interfaces;
using RoadMap.Domain.Models.Roadmaps;

namespace RoadMap.Application.Features.Nodes.Commands;

public record CreateNodeCommand(CreateNodeRequest Request) : IRequest<int>;

public class CreateNodeHandler : IRequestHandler<CreateNodeCommand, int>
{
    private readonly IRepository _repository;

    public CreateNodeHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateNodeCommand request, CancellationToken cancellationToken)
    {
        var node = new Node
        {
            Title = request.Request.Title,
            Description = request.Request.Description,
            Type = request.Request.Type,
            Difficulty = request.Request.Difficulty,
            IsOptional = request.Request.IsOptional
        };

        await _repository.AddAsync(node);
        await _repository.SaveChangesAsync();

        return node.Id;
    }
}