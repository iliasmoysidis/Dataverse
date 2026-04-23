using Backend.Exceptions;

namespace Backend.Modules.Users.Domain.Exceptions;

public sealed class InvalidRoleException : DomainException
{
    public InvalidRoleException()
        : base("users.invalid_role", "Role is invalid.") { }
}
