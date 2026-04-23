using Backend.Modules.Users.Domain;

namespace Backend.Modules.Users.Application.UseCases.Register;

public sealed record RegisterUserCommand(
    string Email,
    string Password,
    string Name,
    string Surname,
    Role Role
);
