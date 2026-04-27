using Backend.Modules.Users.Domain;

namespace Backend.Modules.Users.Application.Security;

public interface IJwtProvider
{
    string Generate(User user);
}
