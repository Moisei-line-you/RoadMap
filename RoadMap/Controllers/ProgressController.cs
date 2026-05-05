using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadMap.Application.Features.Progress.Commands;
using RoadMap.Application.Features.Progress.Queries;

namespace RoadMap.Controllers;

[Authorize]
[ApiController]
[Route("api/roadmap/{roadmapId:int}/progress")]

public class ProgressController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProgressController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private int GetCurrentUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        return int.Parse(claim!.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetProgress(int roadmapId)
    {
        var result = await _mediator.Send(new GetProgressQuery(GetCurrentUserId(), roadmapId));
        return Ok(result);
    }

    [HttpPost("nodes/{nodeId:int}")]
    public async Task<IActionResult> MarkComplete(int roadmapId, int nodeId)
    {
        await _mediator.Send(new MarkNodeCompleteCommand(GetCurrentUserId(), nodeId, roadmapId));
        return NoContent();
    }

    [HttpDelete("nodes/{nodeId:int}")]
    public async Task<IActionResult> UnmarkComplete(int roadmapId, int nodeId)
    {
        await _mediator.Send(new UnmarkNodeCompleteCommand(GetCurrentUserId(), nodeId, roadmapId));
        return NoContent();
    }
}
