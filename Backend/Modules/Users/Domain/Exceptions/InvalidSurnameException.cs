namespace Backend.Modules.Users.Domain.Exceptions;

public sealed class InvalidSurnameException : DomainException
{
    public InvalidSurnameException()
        : base("users.invalid_surname", "Surname is invalid.") { }
}
