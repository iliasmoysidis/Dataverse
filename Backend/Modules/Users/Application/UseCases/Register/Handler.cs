using Backend.Modules.Users.Application.Exceptions;
using Backend.Modules.Users.Application.Ports;
using Backend.Modules.Users.Domain;
using Backend.Shared;

namespace Backend.Modules.Users.Application.UseCases.Register;

public sealed class RegisterUserHandler
{
    private readonly IUserRepository _repo;
    private readonly IPasswordHasher _hasher;
    private readonly IUnitOfWork _uow;

    public RegisterUserHandler(
        IUserRepository repo,
        IPasswordHasher hasher,
        IUnitOfWork uow
    )
    {
        _repo = repo;
        _hasher = hasher;
        _uow = uow;
    }

    public Task Handle(
        RegisterUserCommand command,
        CancellationToken ct
    )
    {
        return _uow.ExecuteAsync(async () =>
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

        }, ct);
    }
}
