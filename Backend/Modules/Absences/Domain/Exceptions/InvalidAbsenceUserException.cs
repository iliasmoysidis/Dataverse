using Backend.Exceptions;

namespace Backend.Modules.Absences.Domain.Exceptions;

public sealed class InvalidAbsenceUserException : DomainException
{
    public InvalidAbsenceUserException()
        : base(
            "absences.invalid_user",
            "User id is invalid."
        )
    {
    }
}
