using Backend.Exceptions;

namespace Backend.Modules.Users.Domain.Exceptions;

public sealed class InvalidEmailException : DomainException
{
    public InvalidEmailException()
        : base("users.invalid_email", "Email is invalid.") { }
}
