using Microsoft.AspNetCore.Mvc;
using RoadMap.Application.Interfaces;
using RoadMap.Domain.Enums;

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
        await _nodeService.AddDependencyAsync(id, request.ToNodeId, request.Type);
        return NoContent();
    }

    [HttpPost("{id:int}/resources")]
    public async Task<IActionResult> AddResource(int id, [FromBody] AddResourceRequest request)
    {
        await _nodeService.AddResourceAsync(id, request.ResourceId);
        return NoContent();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateNode([FromBody] CreateNodeRequest request)
    {
        var id = await _nodeService.CreateNodeAsync(
            request.Title,
            request.Description,
            request.Type,
            request.Difficulty,
            request.IsOptional);

        return CreatedAtAction(nameof(GetNode), new { id }, new { id });
    }

    public record CreateNodeRequest(
        string Title,
        string Description,
        NodeType Type,
        int Difficulty,
        bool IsOptional);
}

public record AddDependencyRequest(int ToNodeId, DependencyType Type);
public record AddResourceRequest(int ResourceId);