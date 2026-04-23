namespace Backend.Modules.Users.Domain.Exceptions;

public sealed class InvalidNameError : DomainError
{
    public InvalidNameError()
        : base("users.invalid_name", "Name is invalid.") { }
}
