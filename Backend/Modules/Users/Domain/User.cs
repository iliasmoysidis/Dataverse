using Backend.Modules.Users.Domain.Exceptions;
using Backend.Modules.Users.Domain.ValueObjects;

namespace Backend.Modules.Users.Domain;

public class User
{
    public int Id { get; private set; }
    public Email Email { get; private set; } = null!;
    public PasswordHash PasswordHash { get; private set; } = null!;

    public Name Name { get; private set; } = null!;
    public Surname Surname { get; private set; } = null!;
    public Role Role { get; private set; }

    private User(Email email, PasswordHash passwordHash, Name name, Surname surname, Role role)
    {
        Email = email;
        PasswordHash = passwordHash;
        Name = name;
        Surname = surname;
        Role = role;
    }

    private User() { }

    public static User Create(
        string email,
        string passwordHash,
        string name,
        string surname,
        Role role
    )
    {
        if (!Enum.IsDefined<Role>(role))
            throw new InvalidRoleException();

        return new User(
            Email.Create(email),
            PasswordHash.Create(passwordHash),
            Name.Create(name),
            Surname.Create(surname),
            role
        );
    }
}
