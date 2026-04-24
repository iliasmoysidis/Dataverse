using Backend.Exceptions;

namespace Backend.Modules.Absences.Domain.Exceptions;

public sealed class AbsenceStartDateInPastException : DomainException
{
    public AbsenceStartDateInPastException()
        : base(
            "absences.start_date_in_past",
            "Start date cannot be in the past."
        )
    {
    }
}
