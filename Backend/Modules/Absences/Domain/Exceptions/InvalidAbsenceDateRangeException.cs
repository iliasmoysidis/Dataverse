using Backend.Exceptions;

namespace Backend.Modules.Absences.Domain.Exceptions;

public sealed class InvalidAbsenceDateRangeException : DomainException
{
    public InvalidAbsenceDateRangeException()
        : base(
            "absences.invalid_date_range",
            "End date cannot be before start date."
        )
    {
    }
}
