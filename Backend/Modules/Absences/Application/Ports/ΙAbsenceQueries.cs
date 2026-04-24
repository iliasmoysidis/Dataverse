using Backend.Modules.Absences.Application.UseCases.GetByUser;
using Backend.Modules.Absences.Application.UseCases.GetPending;
using Backend.Modules.Absences.Application.UseCases.GetPendingByUser;

namespace Backend.Modules.Absences.Application.Ports;

public interface IAbsenceQueries
{
    Task<IReadOnlyCollection<GetPendingAbsencesResult>> GetPendingAsync(CancellationToken ct);
    Task<IReadOnlyCollection<GetPendingAbsencesByUserResult>> GetPendingByUserAsync(int userId, CancellationToken ct);
    Task<IReadOnlyCollection<GetAbsencesByUserResult>> GetByUserAsync(int userId, CancellationToken ct);
}
