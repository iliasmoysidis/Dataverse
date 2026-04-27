using Backend.Exceptions;

namespace Backend.Modules.Absences.Application.Exceptions;

public sealed class CannotCancelOtherUsersAbsenceException : AppException
{
    public CannotCancelOtherUsersAbsenceException()
        : base(
            "absences.cancel.forbidden",
            "You can only cancel your own absence requests."
        )
    {
    }
}
