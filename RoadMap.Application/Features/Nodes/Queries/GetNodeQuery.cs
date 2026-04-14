using MediatR;
using RoadMap.Application.DTOs.Nodes;
using RoadMap.Domain.Exceptions;
using RoadMap.Domain.Interfaces;

namespace RoadMap.Application.Features.Nodes.Queries;

public record GetNodeQuery(int Id) : IRequest<NodeDto>;

public class GetNodeHandler : IRequestHandler<GetNodeQuery, NodeDto>
{
    private readonly IRepository _repository;

    public GetNodeHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<NodeDto> Handle(GetNodeQuery request, CancellationToken cancellationToken)
    {
        var node = await _repository.Nodes.GetFullInfoAsync(request.Id);

        if (node == null)
            throw new NotFoundException("Node", request.Id);

        return new NodeDto(
            node.Id,
            node.Title,
            node.Description
        );
    }
}