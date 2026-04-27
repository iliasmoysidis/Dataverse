namespace Backend.Modules.Users.Api.Requests;

public sealed record LoginUserRequest(
    string Email,
    string Password
);
