using Microsoft.AspNetCore.Mvc;
using RoadMap.Application.DTOs.Nodes;
using RoadMap.Application.Interfaces;

namespace RoadMap.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NodesController : ControllerBase
{
    private readonly INodeService _nodeService;

    public NodesController(INodeService nodeService)
    {
        _nodeService = nodeService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetNode(int id)
    {
        var node = await _nodeService.GetFullNodeAsync(id);
        return Ok(node);
    }

    [HttpPost("{id:int}/dependencies")]
    public async Task<IActionResult> AddDependency(int id, [FromBody] AddDependencyRequest request)
    {
        var dto = request with { FromNodeId = id };
        await _nodeService.AddDependencyAsync(dto);
        return NoContent();
    }

    [HttpPost("{id:int}/resources")]
    public async Task<IActionResult> AddResource(int id, [FromBody] AddResourceRequest request)
    {
        var dto = request with { NodeId = id };
        await _nodeService.AddResourceAsync(dto);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateNode([FromBody] CreateNodeRequest request)
    {
        var id = await _nodeService.CreateNodeAsync(request);

        return CreatedAtAction(nameof(GetNode), new { id }, new { id });
    }
}