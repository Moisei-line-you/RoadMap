using FluentValidation;

namespace RoadMap.Application.Features.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Length(6, 20).WithMessage("Username must be between 6 and 20 characters.");
        
        RuleFor(x => x.Password)    
            .NotEmpty().WithMessage("Password is required.")
            .Length(10, 20).WithMessage("Password must be between 10 and 20 characters.");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.") 
            .EmailAddress().WithMessage("Invalid email address.");
    }
}