using Microsoft.AspNetCore.Mvc;
using RoadMap.Application.DTO.Auth;
using RoadMap.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace RoadMap.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController :  ControllerBase
{
    private readonly IAuthService _authService;
    
    public  AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        var result = await _authService.RegisterAsync(request);
        
        if  (result == "Email already exists")
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        var result = await _authService.LoginAsync(request);

        if (result == null)
            return Unauthorized("Invalid username or password");

        return Ok(result);
    }
}


