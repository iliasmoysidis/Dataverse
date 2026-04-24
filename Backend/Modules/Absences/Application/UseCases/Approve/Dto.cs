namespace Backend.Modules.Absences.Application.UseCases.Approve;

public sealed record ApproveAbsenceResult(
    int Id,
    int UserId,
    DateOnly StartDate,
    DateOnly EndDate,
    int Status
);
