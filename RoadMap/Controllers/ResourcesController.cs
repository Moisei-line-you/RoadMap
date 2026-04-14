using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoadMap.Application.DTOs.Resources;
using RoadMap.Application.Features.Resources.Queries;


namespace RoadMap.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResourcesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResourcesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var resource = await _mediator.Send(new GetResourceByIdQuery(id));
        return Ok(resource);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var resources = await _mediator.Send(new GetAllResourcesQuery());
        return Ok(resources);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateResourceRequest request)
    {
        var id = await _mediator.Send(new CreateResourceCommand(request));

        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteResourceCommand(id));
        return NoContent();
    }
}