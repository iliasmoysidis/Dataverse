namespace Backend.Modules.Absences.Api.Requests;

public sealed record CreateAbsenceRequest(
    DateOnly StartDate,
    DateOnly EndDate
);
