using Backend.Modules.Absences.Domain;

namespace Backend.Modules.Absences.Application.Ports;

public interface IAbsenceRepository
{
    Task AddAsync(Absence absence, CancellationToken ct);
}
