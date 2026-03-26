using RoadMap.Application.DTO.Auth;
using RoadMap.Application.Exceptions;
using RoadMap.Application.Interfaces;
using RoadMap.Models.Users;
using Microsoft.EntityFrameworkCore;
using RoadMap.Data;

namespace RoadMap.Application.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService; 

    public AuthService(AppDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }
    
    public async Task RegisterAsync(RegisterDto dto) 
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
        {
            throw new EmailAlreadyExistsException();
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var newUser = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = passwordHash,
            RoleId = 1
        };
        
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
    }

    public async Task<TokenResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);

        if (user == null  || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            throw new InvalidCredentialsException();
        }
        
        var token = _tokenService.GenerateJwtToken(user);

        return new TokenResponseDto
        {
            Token = token
        };
    }
}