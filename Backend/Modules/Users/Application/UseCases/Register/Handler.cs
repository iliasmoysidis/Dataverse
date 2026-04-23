using Backend.Modules.Users.Application.Exceptions;
using Backend.Modules.Users.Domain;
using Backend.Modules.Users.Ports;

namespace Backend.Modules.Users.Application.UseCases.Register;

public sealed class RegisterUserHandler
{
    private readonly IUserRepository _repo;
    private readonly IPasswordHasher _hasher;

    public RegisterUserHandler(
        IUserRepository repo,
        IPasswordHasher hasher
    )
    {
        _repo = repo;
        _hasher = hasher;
    }

    public async Task<RegisterUserResult> Handle(
        RegisterUserCommand command,
        CancellationToken ct
    )
    {
        var exists = await _repo.ExistsByEmailAsync(command.Email, ct);

        if (exists)
            throw new UserAlreadyExistsException();

        var hash = _hasher.Hash(command.Password);

        var user = User.Create(
            command.Email,
            hash,
            command.Name,
            command.Surname,
            command.Role
        );

        await _repo.AddAsync(user, ct);

        return new RegisterUserResult(
            user.Id,
            user.Email.Value
        );
    }
}
