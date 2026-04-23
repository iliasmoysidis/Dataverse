using Backend.Modules.Users.Domain.Exceptions;

namespace Backend.Modules.Users.Domain.ValueObjects;

public sealed record PasswordHash
{
    public string Value { get; }

    private PasswordHash(string value)
    {
        Value = value;
    }

    public static PasswordHash Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidPasswordHashException();

        return new PasswordHash(value);
    }

    public override string ToString() => Value;
}
