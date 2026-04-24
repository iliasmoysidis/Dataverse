using Backend.Exceptions;

namespace Backend.Modules.Absences.Application.Exceptions;

public sealed class AbsenceNotFoundException : AppException
{
    public AbsenceNotFoundException()
        : base(
            "absences.not_found",
            "Absence was not found."
        )
    {
    }
}
