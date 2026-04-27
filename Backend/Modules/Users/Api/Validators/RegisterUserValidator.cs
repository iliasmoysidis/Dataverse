using Backend.Modules.Users.Api.Requests;
using FluentValidation;

namespace Backend.Modules.Users.Api.Validators;

public sealed class RegisterUserValidator
    : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email format is invalid.")
            .MaximumLength(255)
            .WithMessage("Email cannot exceed 255 characters.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters.")
            .MaximumLength(100)
            .WithMessage("Password cannot exceed 100 characters.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MinimumLength(2)
            .WithMessage("Name must be at least 2 characters.")
            .MaximumLength(100)
            .WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.Surname)
            .NotEmpty()
            .WithMessage("Surname is required.")
            .MinimumLength(2)
            .WithMessage("Surname must be at least 2 characters.")
            .MaximumLength(100)
            .WithMessage("Surname cannot exceed 100 characters.");

        RuleFor(x => x.Role)
            .InclusiveBetween(1, 2)
            .WithMessage("Role must be Employee or Manager.");
    }
}

