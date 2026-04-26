using Backend.Modules.Absences.Domain;

namespace Backend.Modules.Absences.Application.Ports;

public interface IAbsenceRepository
{
    Task AddAsync(Absence absence, CancellationToken ct);

    Task<Absence?> GetByIdAsync(int id, CancellationToken ct);

    Task<bool> ExistsOverlappingAsync(int userId, DateOnly startDate, DateOnly endDate, CancellationToken ct);
}
