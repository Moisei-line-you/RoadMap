using Microsoft.AspNetCore.Mvc;
using RoadMap.Application.DTOs.Roadmaps;
using RoadMap.Application.Interfaces;

namespace RoadMap.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoadmapController : ControllerBase
{
    private readonly IRoadmapService _roadmapService;

    public RoadmapController(IRoadmapService roadmapService)
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
    public async Task<IActionResult> AddNode(int id, [FromBody] AddNodeToRoadmapRequest request)
    {
        var dto = request with { RoadmapId = id };
        await _roadmapService.AddNodeToRoadmapAsync(dto);
        return NoContent();
    }
    
    [HttpGet("{id:int}/available-nodes")]
    public async Task<IActionResult> GetAvailableNodes(
        int id,
        [FromQuery] List<int> completedNodeIds)
    {
        var dto = new GetAvailableNodesRequest(id, completedNodeIds);
        var nodes = await _roadmapService.GetAvailableNodesAsync(dto);
        return Ok(nodes);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateRoadmap([FromBody] CreateRoadmapRequest request)
    {
        var serviceRequest = new CreateRoadmapRequest(
            request.Title,
            request.Description);
        
        var roadmap = await _roadmapService.CreateRoadmap(serviceRequest);
        
        var dto = new RoadmapDto(
            roadmap.Id,
            roadmap.Title,
            roadmap.Description,
            roadmap.Nodes.Select(n => new RoadmapNodeDto(
                n.NodeId,
                n.PositionX,
                n.PositionY
            )).ToList()
        );
        
        return CreatedAtAction(nameof(GetRoadmap), new { id = roadmap.Id }, dto);
    }
}
