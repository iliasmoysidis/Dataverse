namespace Backend.Modules.Absences.Application.UseCases.GetPending;

public sealed record GetPendingAbsencesResult(
    int Id,
    int UserId,
    DateOnly StartDate,
    DateOnly EndDate,
    int Status
);
