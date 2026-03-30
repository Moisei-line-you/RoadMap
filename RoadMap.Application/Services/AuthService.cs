using RoadMap.Application.DTOs.Auth;
using RoadMap.Application.Interfaces;
using RoadMap.Domain.Exceptions;
using RoadMap.Domain.Interfaces;
using RoadMap.Models.Users;

namespace RoadMap.Application.Services;

public class AuthService : IAuthService
{
    private readonly IRepository _repository;
    private readonly ITokenService _tokenService;

    public AuthService(IRepository repository, ITokenService tokenService)
    {
        _repository = repository;
        _tokenService = tokenService;
    }
    
    public async Task RegisterAsync(RegisterDto dto) 
    {
        if (await _repository.Users.EmailExistsAsync(dto.Email))
        {
            throw new EmailAlreadyExistsException();
        }

        var newUser = CreateUser(dto);
        
        await _repository.AddAsync(newUser);
        await _repository.SaveChangesAsync();
    }

    public async Task<TokenResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _repository.Users.GetByUsernameAsync(dto.Username);

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