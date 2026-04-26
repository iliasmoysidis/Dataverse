using Backend.Modules.Absences.Application.Ports;
using Backend.Modules.Absences.Application.UseCases.Search;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Absences.Infrastructure;

public class AbsenceQueries : IAbsenceQueries
{
    private readonly AppDbContext _db;

    public AbsenceQueries(AppDbContext db)
    {
        _db = db;
    }

    public async Task<PagedResult<GetAbsenceResult>> SearchAsync(
    SearchAbsencesQuery query,
    CancellationToken ct)
    {
        var dbQuery =
            from absence in _db.Absences.AsNoTracking()
            join user in _db.Users.AsNoTracking()
                on absence.UserId equals user.Id
            select new
            {
                Absence = absence,
                User = user
            };

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var term = query.Search.ToLower();

            dbQuery = dbQuery.Where(x =>
                x.User.Name.Value.ToLower().Contains(term) ||
                x.User.Surname.Value.ToLower().Contains(term) ||
                x.User.Email.Value.ToLower().Contains(term)
            );
        }

        if (query.Status.HasValue)
            dbQuery = dbQuery.Where(x =>
                (int)x.Absence.Status == query.Status.Value);

        if (query.UserId.HasValue)
            dbQuery = dbQuery.Where(x =>
                x.Absence.UserId == query.UserId.Value);

        if (query.From.HasValue)
            dbQuery = dbQuery.Where(x =>
                x.Absence.StartDate >= query.From.Value);

        if (query.To.HasValue)
            dbQuery = dbQuery.Where(x =>
                x.Absence.EndDate <= query.To.Value);

        var total = await dbQuery.CountAsync(ct);

        var orderedQuery = query.SortBy?.ToLowerInvariant() switch
        {
            "status" => query.Desc
                ? dbQuery.OrderByDescending(x => x.Absence.Status)
                : dbQuery.OrderBy(x => x.Absence.Status),

            "enddate" => query.Desc
                ? dbQuery.OrderByDescending(x => x.Absence.EndDate)
                : dbQuery.OrderBy(x => x.Absence.EndDate),

            "userid" => query.Desc
                ? dbQuery.OrderByDescending(x => x.Absence.UserId)
                : dbQuery.OrderBy(x => x.Absence.UserId),

            "email" => query.Desc
                ? dbQuery.OrderByDescending(x => x.User.Email.Value)
                : dbQuery.OrderBy(x => x.User.Email.Value),

            "name" => query.Desc
                ? dbQuery.OrderByDescending(x => x.User.Name.Value)
                : dbQuery.OrderBy(x => x.User.Name.Value),

            "surname" => query.Desc
                ? dbQuery.OrderByDescending(x => x.User.Surname.Value)
                : dbQuery.OrderBy(x => x.User.Surname.Value),

            _ => query.Desc
                ? dbQuery.OrderByDescending(x => x.Absence.StartDate)
                        .ThenByDescending(x => x.Absence.Id)
                : dbQuery.OrderBy(x => x.Absence.StartDate)
                        .ThenBy(x => x.Absence.Id)
        };

        var page = query.Page < 1 ? 1 : query.Page;
        var limit = query.Limit <= 0 ? 20 : query.Limit;
        var offset = (page - 1) * limit;

        var items = await orderedQuery
            .Skip(offset)
            .Take(limit)
            .Select(x => new GetAbsenceResult(
                x.Absence.Id,
                x.Absence.StartDate,
                x.Absence.EndDate,
                (int)x.Absence.Status,
                new UserInfoResult(
                    x.User.Id,
                    x.User.Email.Value,
                    x.User.Name.Value,
                    x.User.Surname.Value,
                    (int)x.User.Role
                )
            ))
            .ToListAsync(ct);

        return new PagedResult<GetAbsenceResult>(
            items,
            page,
            limit,
            total
        );
    }
}
