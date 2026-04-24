namespace Backend.Infrastructure.Seeds;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        await UserSeeder.SeedAsync(db);
        await AbsenceSeeder.SeedAsync(db);
    }
}
