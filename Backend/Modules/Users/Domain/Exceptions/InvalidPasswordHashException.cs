namespace Backend.Modules.Users.Domain.Exceptions;

public sealed class InvalidPasswordHashException : DomainException
{
    public InvalidPasswordHashException()
        : base("users.invalid_password_hash", "Password hash is invalid.") { }
}
