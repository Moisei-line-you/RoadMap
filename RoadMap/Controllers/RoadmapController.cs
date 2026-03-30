using Microsoft.AspNetCore.Mvc;
using RoadMap.Application.Interfaces;
using RoadMap.Application.Services;

namespace RoadMap.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoadmapsController : ControllerBase
{
    private readonly IRoadmapService _roadmapService;

    public RoadmapsController(IRoadmapService roadmapService)
    {
        _roadmapService = roadmapService;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRoadmap(int id)
    {
        var roadmap = await _roadmapService.GetRoadmapAsync(id);
        return Ok(roadmap);
    }
    
    [HttpPost("{id:int}/nodes")]
    public async Task<IActionResult> AddNode(int id, [FromBody] AddNodeRequest request)
    {
        await _roadmapService.AddNodeToRoadmapAsync(id, request.NodeId, request.X, request.Y);
        return NoContent();
    }
    
    [HttpGet("{id:int}/available-nodes")]
    public async Task<IActionResult> GetAvailableNodes(
        int id,
        [FromQuery] List<int> completedNodeIds)
    {
        var nodes = await _roadmapService.GetAvailableNodesAsync(id, completedNodeIds);
        return Ok(nodes);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateRoadmap([FromBody] RoadmapService.CreateRoadmapRequest request)
    {
        
        var roadmap = await _roadmapService.CreateRoadmap(request);
        
        return CreatedAtAction(nameof(GetRoadmap), new { id = roadmap.Id }, roadmap);
    }
}

public record AddNodeRequest(int NodeId, double X, double Y);