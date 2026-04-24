using Backend.Exceptions;

namespace Backend.Modules.Users.Application.Exceptions;

public sealed class InvalidCredentialsException : AppException
{
    public InvalidCredentialsException()
        : base("users.invalid_credentials", "Invalid credentials.")
    { }
}
