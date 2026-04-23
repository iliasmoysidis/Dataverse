using Backend.Modules.Users.Api.Register;
using Backend.Modules.Users.Application.UseCases.Register;
using Backend.Modules.Users.Domain;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public sealed class UsersController : ControllerBase
{
    private readonly RegisterUserHandler _register;

    public UsersController(RegisterUserHandler register)
    {
        _register = register;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken ct)
    {
        var command = new RegisterUserCommand(
            request.Email,
            request.Password,
            request.Name,
            request.Surname,
            (Role)request.Role
        );

        var result = await _register.Handle(command, ct);

        return Created($"/api/users/{result.Id}", result);
    }
}
