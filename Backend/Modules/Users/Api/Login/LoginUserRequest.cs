namespace Backend.Modules.Users.Api.Login;

public sealed record LoginUserRequest(
    string Email,
    string Password
);
