using Backend.Exceptions;

namespace Backend.Modules.Absences.Domain.Exceptions;

public sealed class InvalidAbsenceStatusTransitionException : DomainException
{
    public InvalidAbsenceStatusTransitionException()
        : base(
            "absences.invalid_status_transition",
            "Absence status transition is invalid."
        )
    { }
}
