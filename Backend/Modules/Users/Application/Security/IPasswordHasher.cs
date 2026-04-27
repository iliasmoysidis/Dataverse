namespace Backend.Modules.Users.Security;

public interface IPasswordHasher
{
    string Hash(string password);
}
