using MediatR;
using RoadMap.Application.Exceptions;
using RoadMap.Domain.Interfaces;
using RoadMap.Models.Users;

namespace RoadMap.Application.Features.Auth.Commands.Register;

public record RegisterCommand (string Username, string Email, string Password) : IRequest<Unit>;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
{
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.EmailExistsAsync(request.Email))
        {
            throw new EmailAlreadyExistsException();
        }

        var newUser = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            RoleId = 1
        };

        await _userRepository.AddUserAsync(newUser);
        await _userRepository.SaveChangesAsync();
        
        return Unit.Value;
    }
}