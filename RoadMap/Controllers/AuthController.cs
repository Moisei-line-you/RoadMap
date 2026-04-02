using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoadMap.Application.DTOs.Auth;
using RoadMap.Application.Exceptions;
using RoadMap.Application.Features.Auth.Commands.Register;
using RoadMap.Application.Features.Auth.Queries.Login;


namespace RoadMap.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        try
        {
            await _mediator.Send(command);
            
            return Ok(new { message = "Registration successful" });
        }
        catch (EmailAlreadyExistsException ex)
        {
            return Conflict(new { message = ex.Message }); 
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginQuery query)
    {
        try
        {
            var result = await _mediator.Send(query);
            
            return Ok(result); 
        }
        catch (InvalidCredentialsException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}