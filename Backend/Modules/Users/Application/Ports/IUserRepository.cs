using Backend.Modules.Users.Domain;

namespace Backend.Modules.Users.Application.Ports;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken ct);

    Task<bool> ExistsByEmailAsync(string email, CancellationToken ct);

    Task AddAsync(User user, CancellationToken ct);
}
