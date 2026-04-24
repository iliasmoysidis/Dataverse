namespace Backend.Modules.Users.Application.UseCases.Login;

public sealed record LoginUserCommand(string Email, string Password);
