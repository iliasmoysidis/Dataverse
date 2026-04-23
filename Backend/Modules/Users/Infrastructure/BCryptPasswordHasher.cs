using Backend.Modules.Users.Application;

namespace Backend.Modules.Users.Infrastructure;

public sealed class BCryptPasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}
