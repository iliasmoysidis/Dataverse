using Backend.Modules.Absences.Application.UseCases.GetByUser;
using Backend.Modules.Absences.Application.UseCases.GetPending;

namespace Backend.Modules.Absences.Application.Ports;

public interface IAbsenceQueries
{
    Task<IReadOnlyCollection<GetAbsenceResult>> GetAbsencesAsync(CancellationToken ct);
    Task<IReadOnlyCollection<GetAbsencesByUserResult>> GetByUserAsync(int userId, CancellationToken ct);
}
