namespace Backend.Modules.Users.Application.Exceptions;

public sealed class UserAlreadyExistsException : ApplicationException
{
    public UserAlreadyExistsException()
        : base("user.already_exists", "User with this email already exists.")
    { }
}
