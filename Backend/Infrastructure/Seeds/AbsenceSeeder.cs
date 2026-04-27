// Infrastructure/Seeds/AbsenceSeeder.cs
using Backend.Modules.Absences.Domain;
using Backend.Modules.Users.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Seeds;

public static class AbsenceSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (db.Absences.Any())
            return;

        var employees = await db.Users
            .Where(x => x.Role == Role.Employee)
            .ToListAsync();

        var random = new Random();
        var absences = new List<Absence>();

        foreach (var user in employees)
        {
            var existingRanges = new List<(DateOnly Start, DateOnly End, int Status)>();

            for (int i = 0; i < 6; i++)
            {
                var created = false;

                while (!created)
                {
                    var startOffset = random.Next(1, 160);
                    var duration = random.Next(1, 8);

                    var start = DateOnly.FromDateTime(
                        DateTime.UtcNow.AddDays(startOffset)
                    );

                    var end = start.AddDays(duration);

                    var statusRoll = random.Next(1, 4); // 1 pending 2 approved 3 rejected

                    var newStatus =
                        statusRoll == 2 ? 2 :
                        statusRoll == 3 ? 3 : 1;

                    var overlaps = existingRanges.Any(x =>
                        (x.Status == 1 || x.Status == 2) &&
                        (newStatus == 1 || newStatus == 2) &&
                        start <= x.End &&
                        end >= x.Start
                    );

                    if (overlaps)
                        continue;

                    var absence = Absence.Create(user.Id, start, end);

                    if (newStatus == 2)
                        absence.Approve();
                    else if (newStatus == 3)
                        absence.Reject();

                    absences.Add(absence);

                    existingRanges.Add((start, end, newStatus));

                    created = true;
                }
            }
        }

        db.Absences.AddRange(absences);
        await db.SaveChangesAsync();
    }
}
