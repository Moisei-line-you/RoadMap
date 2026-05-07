using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadMap.Application.Features.Progress.Commands;
using RoadMap.Application.Features.Progress.Queries;

namespace RoadMap.Controllers;

[Authorize]
[ApiController]
[Route("api/roadmaps/{roadmapId:int}/progress")]

public class ProgressController : ControllerBase    
{
    private readonly IMediator _mediator;
    public ProgressController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private bool TryGetCurrentUserId(out int userId)
    {
        userId = default;
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        return claim is not null && int.TryParse(claim.Value, out userId);
    }

    [HttpGet]
    public async Task<IActionResult> GetProgress(int roadmapId)
    {
        if (!TryGetCurrentUserId(out var userId))
        {
            return Unauthorized();
        }

        var result = await _mediator.Send(new GetProgressQuery(userId, roadmapId));
        return Ok(result);
    }

    [HttpPost("nodes/{nodeId:int}")]
    public async Task<IActionResult> MarkComplete(int roadmapId, int nodeId)
    {
        if (!TryGetCurrentUserId(out var userId))
        {
            return Unauthorized();
        }
        await _mediator.Send(new MarkNodeCompleteCommand(userId, nodeId, roadmapId));
        return NoContent();
    }

    [HttpDelete("nodes/{nodeId:int}")]
    public async Task<IActionResult> UnmarkComplete(int roadmapId, int nodeId)
    {
        if (!TryGetCurrentUserId(out var userId))
        {
            return Unauthorized();
        }
        await _mediator.Send(new UnmarkNodeCompleteCommand(userId,roadmapId, nodeId));
        return NoContent();
    }
}
