namespace Backend.Modules.Users.Domain.Exceptions;

public sealed class InvalidEmailException : DomainError
{
    public InvalidEmailException()
        : base("users.invalid_email", "Email is invalid.") { }
}
