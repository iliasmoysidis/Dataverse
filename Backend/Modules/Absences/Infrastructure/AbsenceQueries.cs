using Backend.Modules.Absences.Application.Ports;
using Backend.Modules.Absences.Application.UseCases.GetByUser;
using Backend.Modules.Absences.Application.UseCases.GetPending;
using Backend.Modules.Absences.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Absences.Infrastructure;

public class AbsenceQueries : IAbsenceQueries
{
    private readonly AppDbContext _db;

    public AbsenceQueries(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyCollection<GetAbsencesByUserResult>> GetByUserAsync(int userId, CancellationToken ct)
    {
        return await _db.Absences
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderBy(x => x.StartDate)
            .Select(x => new GetAbsencesByUserResult(
                x.Id,
                x.UserId,
                x.StartDate,
                x.EndDate,
                (int)x.Status
            ))
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyCollection<GetAbsenceResult>> GetAbsencesAsync(CancellationToken ct)
    {
        return await _db.Absences
            .AsNoTracking()
            .Where(x => x.Status == Status.Pending)
            .OrderBy(x => x.StartDate)
            .Select(x => new GetAbsenceResult(
                x.Id,
                x.UserId,
                x.StartDate,
                x.EndDate,
                (int)x.Status
            ))
            .ToListAsync(ct);
    }
}
