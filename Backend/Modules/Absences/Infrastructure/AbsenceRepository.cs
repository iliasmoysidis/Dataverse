using Backend.Modules.Absences.Application.Ports;
using Backend.Modules.Absences.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Absences.Infrastructure;

public sealed class AbsenceRepository : IAbsenceRepository
{
    private readonly AppDbContext _db;

    public AbsenceRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Absence absence, CancellationToken ct)
    {
        await _db.Absences.AddAsync(absence, ct);
    }

    public Task<Absence?> GetByIdAsync(int id, CancellationToken ct)
    {
        return _db.Absences.FirstOrDefaultAsync(x => x.Id == id, ct);
    }
}
