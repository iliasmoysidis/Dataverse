using Backend.Modules.Users.Domain.ValueObjects;

namespace Backend.Modules.Users.Domain;

public class User
{
    public int Id { get; set; }
    public Email Email { get; set; }
    public PasswordHash PasswordHash { get; set; }

    public Name Name { get; set; }
    public Surname Surname { get; set; }
    public Role Role { get; set; }

    private User(Email email, PasswordHash passwordHash, Name name, Surname surname, Role role)
    {
        Email = email;
        PasswordHash = passwordHash;
        Name = name;
        Surname = surname;
        Role = role;
    }

    public static User Create(
        string email,
        string passwordHash,
        string name,
        string surname,
        Role role
    )
    {
        return new User(
            Email.Create(email),
            PasswordHash.Create(passwordHash),
            Name.Create(name),
            Surname.Create(surname),
            role
        );
    }
}
