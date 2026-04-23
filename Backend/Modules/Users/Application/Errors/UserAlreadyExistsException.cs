namespace Backend.Modules.Users.Application.Exceptions;

public sealed class UserAlreadyExistsException : ApplicationLayerException
{
    public UserAlreadyExistsException()
        : base("users.already_exists", "User with this email already exists.")
    { }
}
