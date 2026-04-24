namespace Backend.Modules.Absences.Api.Requests.Create;

public sealed record CreateAbsenceRequest(
    DateOnly StartDate,
    DateOnly EndDate
);
