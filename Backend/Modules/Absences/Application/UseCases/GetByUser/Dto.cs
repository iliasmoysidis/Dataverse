namespace Backend.Modules.Absences.Application.UseCases.GetByUser;

public sealed record GetAbsencesByUserResult(
    int Id,
    int UserId,
    DateOnly StartDate,
    DateOnly EndDate,
    int Status
);
