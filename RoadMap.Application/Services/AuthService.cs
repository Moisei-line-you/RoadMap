using RoadMap.Application.DTO.Auth;
using RoadMap.Application.Exceptions;
using RoadMap.Application.Interfaces;
using RoadMap.Models.Users;
using Microsoft.EntityFrameworkCore;
using RoadMap.Data;

namespace RoadMap.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }
    
    public async Task RegisterAsync(RegisterDto dto) 
    {
        if (await _userRepository.EmailExistsAsync(dto.Email))
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
        
        await _userRepository.AddUserAsync(newUser);
    }

    public async Task<TokenResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username);

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