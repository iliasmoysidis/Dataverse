namespace Backend.Modules.Users.Domain.Exceptions;

public sealed class InvalidPasswordHashError : DomainError
{
    public InvalidPasswordHashError()
        : base("users.invalid_password_hash", "Password hash is invalid.") { }
}
