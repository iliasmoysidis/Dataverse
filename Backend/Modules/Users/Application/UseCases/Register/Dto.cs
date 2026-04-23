namespace Backend.Modules.Users.Application.UseCases.Register;

public sealed record RegisterUserResult(
    int Id,
    string Email
);
