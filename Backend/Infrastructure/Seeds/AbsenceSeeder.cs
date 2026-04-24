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
            // 3 absences per employee
            for (int i = 0; i < 3; i++)
            {
                var startOffset = random.Next(2, 60);
                var duration = random.Next(1, 8);

                var start = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(startOffset));
                var end = start.AddDays(duration);

                var absence = Absence.Create(user.Id, start, end);

                var statusRoll = random.Next(1, 4);

                if (statusRoll == 2)
                    absence.Approve();
                else if (statusRoll == 3)
                    absence.Reject();

                absences.Add(absence);
            }
        }

        db.Absences.AddRange(absences);
        await db.SaveChangesAsync();
    }
}
