using Backend.Modules.Users.Domain;

namespace Backend.Modules.Users.Ports;

public interface IUserRepository
{
    Task<bool> ExistsByEmailAsync(string email, CancellationToken ct);

    Task AddAsync(User user, CancellationToken ct);
}
