namespace Backend.Modules.Users.Application;

public interface IPasswordHasher
{
    string Hash(string password);
}
