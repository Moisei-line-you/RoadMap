using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoadMap.Application.DTOs.Roadmaps;
using RoadMap.Application.Features.Roadmaps.Commands;
using RoadMap.Application.Features.Roadmaps.Queries;

namespace RoadMap.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoadmapsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoadmapsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRoadmap(int id)
    {
        var result = await _mediator.Send(new GetRoadmapQuery(id));
        if (!result.IsSuccess)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{id:int}/available-nodes")]
    public async Task<IActionResult> GetAvailableNodes(int id, [FromQuery] List<int> completedNodeIds)
    {
        var result = await _mediator.Send(new GetAvailableNodesQuery(id, completedNodeIds));
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoadmap([FromBody] CreateRoadmapRequest request)
    {
        var result = await _mediator.Send(new CreateRoadmapCommand(request));
        return CreatedAtAction(nameof(GetRoadmap), new { id = result.Value.Id }, result.Value);
    }

    [HttpPost("{id:int}/node-assignments")]
    public async Task<IActionResult> AssignNode(int id, [FromBody] AddNodeToRoadmapRequest request)
    {
        var dto = request with { RoadmapId = id };
        await _mediator.Send(new AssignNodeToRoadmapCommand(dto));
        return NoContent();
    }
}