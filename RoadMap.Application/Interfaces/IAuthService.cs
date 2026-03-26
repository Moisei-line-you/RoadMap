using RoadMap.Application.DTO.Auth;

namespace RoadMap.Application.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(RegisterDto dto);
    Task<TokenResponseDto> LoginAsync(LoginDto dto);
}