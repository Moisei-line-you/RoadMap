using Microsoft.AspNetCore.Mvc;
using RoadMap.Application.DTO.Auth;
using RoadMap.Application.Exceptions;       
using RoadMap.Application.Interfaces;

namespace RoadMap.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        try
        {
            await _authService.RegisterAsync(request);
            
            return Ok(new { message = "Registration successful" });
        }
        catch (EmailAlreadyExistsException ex)
        {
            return Conflict(new { message = ex.Message }); 
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        try
        {
            var result = await _authService.LoginAsync(request);
            
            return Ok(result); 
        }
        catch (InvalidCredentialsException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}