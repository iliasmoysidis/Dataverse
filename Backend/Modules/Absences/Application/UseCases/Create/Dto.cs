namespace Backend.Modules.Absences.Application.UseCases.Create;

public sealed record CreateAbsenceResult(
    int Id,
    int UserId,
    DateOnly StartDate,
    DateOnly EndDate,
    int Status
);
