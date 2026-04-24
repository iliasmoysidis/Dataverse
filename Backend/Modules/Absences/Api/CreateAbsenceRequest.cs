namespace Backend.Modules.Absences.Api.Requests;

public sealed record CreateAbsenceRequest(
    int UserId,
    DateOnly StartDate,
    DateOnly EndDate
);
