using Microsoft.AspNetCore.Mvc;
using RoadMap.Application.DTOs.Roadmaps;
using RoadMap.Application.Interfaces;

namespace RoadMap.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResourceController : ControllerBase
{
    private readonly IResourceService _resourceService;

    public ResourceController(IResourceService resourceService)
    {
        _resourceService = resourceService;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var resource = await _resourceService.GetByIdAsync(id);
        return Ok(resource);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var resources = await _resourceService.GetAllAsync();
        return Ok(resources);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateResourceRequest request)
    {
        var id = await _resourceService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            new { id }
        );
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _resourceService.DeleteAsync(id);
        return NoContent();
    }
}