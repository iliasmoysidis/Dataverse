namespace Backend.Modules.Absences.Application.UseCases.GetPendingByUser;

public sealed record GetPendingAbsencesByUserResult(
    int Id,
    int UserId,
    DateOnly StartDate,
    DateOnly EndDate,
    int Status
);
