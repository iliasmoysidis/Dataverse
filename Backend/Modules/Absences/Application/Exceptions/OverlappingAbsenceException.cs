namespace Backend.Exceptions;

public sealed class OverlappingAbsenceException : AppException
{
    public OverlappingAbsenceException()
        : base(
            "absence.overlap",
            "You already have an absence request for the selected dates."
        )
    {
    }
}
