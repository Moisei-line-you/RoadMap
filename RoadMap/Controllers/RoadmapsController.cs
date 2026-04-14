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
        var roadmap = await _mediator.Send(new GetRoadmapQuery(id));
        return Ok(roadmap);
    }

    [HttpGet("{id:int}/available-nodes")]
    public async Task<IActionResult> GetAvailableNodes(int id, [FromQuery] List<int> completedNodeIds)
    {
        var nodes = await _mediator.Send(new GetAvailableNodesQuery(id, completedNodeIds));
        return Ok(nodes);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoadmap([FromBody] CreateRoadmapRequest request)
    {
        var roadmap = await _mediator.Send(new CreateRoadmapCommand(request));

        return CreatedAtAction(
            nameof(GetRoadmap),
            new { id = roadmap.Id },
            roadmap
        );
    }
    
    [HttpPost("{id:int}/node-assignments")]
    public async Task<IActionResult> AssignNode(int id, [FromBody] AddNodeToRoadmapRequest request)
    {
        var command = request with { RoadmapId = id };

        await _mediator.Send(new AssignNodeToRoadmapCommand(command));

        return NoContent();
    }
}