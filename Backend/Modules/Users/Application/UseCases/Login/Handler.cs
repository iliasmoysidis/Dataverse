using Backend.Modules.Users.Application.Exceptions;
using Backend.Modules.Users.Application.Ports;
using Backend.Modules.Users.Application.Security;

namespace Backend.Modules.Users.Application.UseCases.Login;

public sealed class LoginUserHandler
{
    private readonly IUserRepository _repo;
    private readonly IJwtProvider _jwt;

    public LoginUserHandler(IUserRepository repo, IJwtProvider jwt)
    {
        _repo = repo;
        _jwt = jwt;
    }

    public async Task<LoginUserResult> Handle(
        LoginUserCommand command,
        CancellationToken ct
    )
    {
        var user = await _repo.GetByEmailAsync(command.Email, ct);

        if (user == null)
            throw new UserNotFoundException();

        var valid = BCrypt.Net.BCrypt.Verify(
            command.Password,
            user.PasswordHash.Value
        );

        if (!valid)
            throw new InvalidCredentialsException();

        var token = _jwt.Generate(user);

        return new LoginUserResult(token);
    }
}
