namespace Backend.Modules.Absences.Application.UseCases.Reject;

public sealed record RejectAbsenceResult(
    int Id,
    int UserId,
    DateOnly StartDate,
    DateOnly EndDate,
    int Status
);
