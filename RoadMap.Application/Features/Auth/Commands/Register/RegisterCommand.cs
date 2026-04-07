using MediatR;
using RoadMap.Domain.Exceptions;
using RoadMap.Domain.Interfaces;
using RoadMap.Models.Users;

namespace RoadMap.Application.Features.Auth.Commands.Register;

public record RegisterCommand (string Username, string Email, string Password) : IRequest<Unit>;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
{
    private readonly IRepository _repository;

    public RegisterCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _repository.Users.EmailExistsAsync(request.Email))
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

        await _repository.AddAsync(newUser);
        await _repository.SaveChangesAsync();
        
        return Unit.Value;
    }
}