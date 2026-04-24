using Backend.Modules.Absences.Application.UseCases.GetPending;

namespace Backend.Modules.Absences.Application.Ports;

public interface IAbsenceQueries
{
    Task<IReadOnlyCollection<GetPendingAbsencesResult>> GetPendingAsync(CancellationToken ct);
}
