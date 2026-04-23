namespace Backend.Modules.Users.Domain.Exceptions;

public sealed class InvalidSurnameError : DomainError
{
    public InvalidSurnameError()
        : base("users.invalid_surname", "Surname is invalid.") { }
}
