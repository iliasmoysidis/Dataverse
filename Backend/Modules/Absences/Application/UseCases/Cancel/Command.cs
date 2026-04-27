namespace Backend.Modules.Absences.Application.UseCases.Cancel;

public sealed record CancelAbsenceCommand(int Id, int CurrentUserId);
