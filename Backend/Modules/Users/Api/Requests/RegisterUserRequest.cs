namespace Backend.Modules.Users.Api.Requests;

public sealed record RegisterUserRequest(
    string Email,
    string Password,
    string Name,
    string Surname,
    int Role
);
