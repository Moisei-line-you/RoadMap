using MediatR;
using RoadMap.Application.DTOs.Auth;
using RoadMap.Application.Interfaces;
using RoadMap.Domain.Exceptions;
using RoadMap.Domain.Interfaces;

namespace RoadMap.Application.Features.Auth.Queries.Login;

public record LoginQuery(string Username, string Password) : IRequest<TokenResponseDto>;

public class LoginQueryHandler : IRequestHandler<LoginQuery, TokenResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    
    public LoginQueryHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<TokenResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
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