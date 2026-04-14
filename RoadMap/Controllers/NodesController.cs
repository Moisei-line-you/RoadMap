using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoadMap.Application.DTOs.Nodes;
using RoadMap.Application.Features.Nodes.Commands;
using RoadMap.Application.Features.Nodes.Queries;

namespace RoadMap.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NodesController : ControllerBase
{
    private readonly IMediator _mediator;

    public NodesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetNode(int id)
    {
        var node = await _mediator.Send(new GetNodeQuery(id));
        return Ok(node);
    }

    [HttpPost("{id:int}/dependencies")]
    public async Task<IActionResult> AddDependency(int id, [FromBody] AddDependencyRequest request)
    {
        var command = request with { FromNodeId = id };

        await _mediator.Send(new AddDependencyCommand(command));

        return NoContent();
    }

    [HttpPost("{id:int}/resources")]
    public async Task<IActionResult> AddResource(int id, [FromBody] AddResourceRequest request)
    {
        var command = request with { NodeId = id };

        await _mediator.Send(new AddResourceCommand(command));

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateNode([FromBody] CreateNodeRequest request)
    {
        var id = await _mediator.Send(new CreateNodeCommand(request));

        return CreatedAtAction(nameof(GetNode), new { id }, new { id });
    }
}