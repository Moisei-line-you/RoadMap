using RoadMap.Application.DTOs.Auth;
using RoadMap.Application.Exceptions;
using RoadMap.Application.Interfaces;
using RoadMap.Domain.Interfaces;
using RoadMap.Models.Users;

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

        var newUser = CreateUser(dto);
        
        await _userRepository.AddUserAsync(newUser);
        await _userRepository.SaveChangesAsync();
    }

    public async Task<TokenResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username);

        if (user == null  || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            throw new InvalidCredentialsException();
        }
        
        return new TokenResponseDto
        {
            Token = _tokenService.GenerateJwtToken(user)
        };
    }

    private User CreateUser(RegisterDto dto)
    {
        return new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            RoleId = 1
        };
    }
    
}