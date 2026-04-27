namespace Backend.Modules.Absences.Application.UseCases.Cancel;

public sealed record CancelAbsenceResult(
    int Id,
    int UserId,
    DateOnly StartDate,
    DateOnly EndDate,
    int Status
);
