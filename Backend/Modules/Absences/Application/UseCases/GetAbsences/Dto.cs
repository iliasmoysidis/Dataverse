namespace Backend.Modules.Absences.Application.UseCases.GetPending;

public sealed record GetAbsenceResult(
    int Id,
    int UserId,
    DateOnly StartDate,
    DateOnly EndDate,
    int Status
);
