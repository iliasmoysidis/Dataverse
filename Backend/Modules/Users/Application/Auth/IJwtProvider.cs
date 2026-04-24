using Backend.Modules.Users.Domain;

namespace Backend.Modules.Users.Application.Auth;

public interface IJwtProvider
{
    string Generate(User user);
}
