using Backend.Exceptions;

namespace Backend.Modules.Users.Application.Exceptions;

public sealed class UserNotFoundException : AppException
{
    public UserNotFoundException()
        : base("users.not_found", "User with this email not found.")
    { }
}
