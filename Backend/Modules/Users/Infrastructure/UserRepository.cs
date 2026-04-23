using Backend.Modules.Users.Domain;
using Backend.Modules.Users.Domain.ValueObjects;
using Backend.Modules.Users.Ports;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Users.Infrastructure;

public sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(User user, CancellationToken ct)
    {
        await _db.Users.AddAsync(user, ct);
        await _db.SaveChangesAsync(ct);
    }

    public Task<bool> ExistsByEmailAsync(string email, CancellationToken ct)
    {
        var normalized = Email.Create(email);
        return _db.Users.AnyAsync(x => x.Email.Value == normalized.Value, ct);
    }
}
