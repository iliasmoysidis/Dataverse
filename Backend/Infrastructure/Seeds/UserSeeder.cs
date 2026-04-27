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

            // Employees (duplicate first names intentionally)
            User.Create("john.employee1@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "John", "Mousidis", Role.Employee),
            User.Create("john.employee2@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "John", "Papas", Role.Employee),

            User.Create("maria1@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Maria", "Papadopoulou", Role.Employee),
            User.Create("maria2@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Maria", "Kosta", Role.Employee),

            User.Create("nikos1@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Nikos", "Georgiou", Role.Employee),
            User.Create("nikos2@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Nikos", "Danos", Role.Employee),

            User.Create("anna1@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Anna", "Kosta", Role.Employee),
            User.Create("anna2@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Anna", "Markou", Role.Employee),

            User.Create("giorgos1@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Giorgos", "Lazarou", Role.Employee),
            User.Create("giorgos2@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Giorgos", "Petrou", Role.Employee),

            User.Create("eleni1@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Eleni", "Dimitriou", Role.Employee),
            User.Create("eleni2@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Eleni", "Sarris", Role.Employee),

            User.Create("petros@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Petros", "Kanelos", Role.Employee),
            User.Create("sofia@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Sofia", "Nikolaou", Role.Employee),
            User.Create("kostas@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Kostas", "Vlachos", Role.Employee),
            User.Create("dimitra@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Dimitra", "Ioannou", Role.Employee),
            User.Create("panos@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Panos", "Alexiou", Role.Employee),
            User.Create("vasiliki@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Vasiliki", "Riga", Role.Employee),
            User.Create("stelios@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Stelios", "Kyriazis", Role.Employee),
            User.Create("christina@dataverse.com", BCrypt.Net.BCrypt.HashPassword("123456"), "Christina", "Meli", Role.Employee)
        };

        db.Users.AddRange(users);
        await db.SaveChangesAsync();
    }
}
