using Backend.Modules.Absences.Domain.Exceptions;

namespace Backend.Modules.Absences.Domain;

public class Absence
{
    public int Id { get; private set; }

    public int UserId { get; private set; }

    public DateOnly StartDate { get; private set; }

    public DateOnly EndDate { get; private set; }

    public Status Status { get; private set; }

    private Absence() { }

    private Absence(
        int userId,
        DateOnly startDate,
        DateOnly endDate,
        Status status
    )
    {
        UserId = userId;
        StartDate = startDate;
        EndDate = endDate;
        Status = status;
    }

    public static Absence Create(
        int userId,
        DateOnly startDate,
        DateOnly endDate
    )
    {
        if (userId <= 0)
            throw new InvalidAbsenceUserException();

        if (endDate < startDate)
            throw new InvalidAbsenceDateRangeException();

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        if (startDate < today)
            throw new AbsenceStartDateInPastException();

        return new Absence(
            userId,
            startDate,
            endDate,
            Status.Pending
        );
    }

    public void Approve()
    {
        if (Status != Status.Pending)
            throw new InvalidAbsenceStatusTransitionException();

        Status = Status.Approved;
    }

    public void Reject()
    {
        if (Status != Status.Pending)
            throw new InvalidAbsenceStatusTransitionException();

        Status = Status.Rejected;
    }

    public void Cancel()
    {
        if (Status != Status.Pending)
            throw new InvalidAbsenceStatusTransitionException();
    }
}
