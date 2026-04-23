namespace Backend.Modules.Users.Domain.Exceptions;

public sealed class InvalidNameException : DomainException
{
    public InvalidNameException()
        : base("users.invalid_name", "Name is invalid.") { }
}
