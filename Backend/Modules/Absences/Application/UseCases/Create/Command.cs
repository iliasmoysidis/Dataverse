namespace Backend.Modules.Absences.Application.UseCases.Create;

public sealed record CreateAbsenceCommand(
    int UserId,
    DateOnly StartDate,
    DateOnly EndDate
);
