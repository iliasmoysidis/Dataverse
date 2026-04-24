using Backend.Modules.Users.Api.Login;
using Backend.Modules.Users.Api.Register;
using Backend.Modules.Users.Application.UseCases.Login;
using Backend.Modules.Users.Application.UseCases.Register;
using Backend.Modules.Users.Domain;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public sealed class UsersController : ControllerBase
{
    private readonly RegisterUserHandler _register;
    private readonly LoginUserHandler _login;

    public UsersController(
        RegisterUserHandler register,
        LoginUserHandler login
        )
    {
        _register = register;
        _login = login;
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

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserRequest request,
        CancellationToken ct
    )
    {
        var command = new LoginUserCommand(request.Email, request.Password);
        var result = await _login.Handle(command, ct);

        return Ok(result);
    }
}
