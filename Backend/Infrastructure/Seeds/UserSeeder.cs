// Infrastructure/Seeds/UserSeeder.cs
using Backend.Modules.Users.Domain;

namespace Backend.Infrastructure.Seeds;

public static class UserSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (db.Users.Any())
            return;

        var users = new[]
        {
            // Managers
            User.Create("manager@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "John", "Smith", Role.Manager),
            User.Create("manager2@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Helen", "Brown", Role.Manager),

            // Employees
            User.Create("ilias@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Ilias", "Mousidis", Role.Employee),
            User.Create("maria@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Maria", "Papadopoulou", Role.Employee),
            User.Create("nikos@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Nikos", "Georgiou", Role.Employee),
            User.Create("anna@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Anna", "Kosta", Role.Employee),
            User.Create("giorgos@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Giorgos", "Lazarou", Role.Employee),
            User.Create("eleni@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Eleni", "Dimitriou", Role.Employee),
            User.Create("petros@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Petros", "Kanelos", Role.Employee),
            User.Create("sofia@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Sofia", "Nikolaou", Role.Employee),
            User.Create("kostas@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Kostas", "Vlachos", Role.Employee),
            User.Create("dimitra@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Dimitra", "Ioannou", Role.Employee),
            User.Create("panos@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Panos", "Alexiou", Role.Employee),
            User.Create("vasiliki@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Vasiliki", "Markou", Role.Employee),
            User.Create("stelios@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Stelios", "Kyriazis", Role.Employee),
            User.Create("christina@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Christina", "Sarris", Role.Employee)
        };

        db.Users.AddRange(users);
        await db.SaveChangesAsync();
    }
}
