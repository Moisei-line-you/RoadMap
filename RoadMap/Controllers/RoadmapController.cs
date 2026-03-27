using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadMap.Application.DTO.Roadmaps;
using RoadMap.Application.Interfaces;

namespace RoadMap.Controllers;


[Route("api/[controller]")]
[ApiController]
public class RoadmapController : ControllerBase
{
    private readonly IRoadmapService _roadmapService;

    public RoadmapController(IRoadmapService roadmapService)
    {
        _roadmapService = roadmapService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoadmapListDto>>> GetAll([FromQuery] string? search)
    {
        var roadmaps = await _roadmapService.GetAllRoadmapsAsync(search);
        return Ok(roadmaps);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoadmapDetailDto>> GetById(Guid id)
    {
        var roadmap = await _roadmapService.GetRoadmapByIdAsync(id);
        if (roadmap == null) return NotFound();

        return Ok(roadmap);
    }

    [Authorize]
    [HttpGet("my")]
    public async Task<ActionResult<IEnumerable<RoadmapListDto>>> GetMyRoadmaps()
    {
        var userId = GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        var myRoadmaps = await _roadmapService.GetRoadmapsByUserIdAsync(userId);
        
        return Ok(myRoadmaps);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RoadmapListDto>> Create(RoadmapCreateDto dto)
    {
        var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString)) return Unauthorized();
        var userId = Guid.Parse(userIdString);

        var result = await _roadmapService.CreateRoadmapAsync(dto, userId);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, RoadmapUpdateDto dto)
    {
        var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString)) return Unauthorized();
        var userId = Guid.Parse(userIdString);

        var updated = await _roadmapService.UpdateRoadmapAsync(id, dto, userId);
        if (!updated) return Forbid();

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        var deleted = await _roadmapService.DeleteRoadmapAsync(id, userId);
        if (!deleted) return Forbid();

        return NoContent();
    }

    private Guid GetUserId()
    {
        var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (Guid.TryParse(userIdString, out var userId))
        {
            return userId;
        }
        return Guid.Empty;
    }
    
    [Authorize]
    [HttpGet("{id}/is-owner")]
    public async Task<ActionResult<bool>> CheckOwnership(Guid id)
    {
        var userId = GetUserId();
        if (userId == Guid.Empty) return Unauthorized();

        var isOwner = await _roadmapService.IsOwnerAsync(id, userId);
        
        return Ok(isOwner);
    }
}