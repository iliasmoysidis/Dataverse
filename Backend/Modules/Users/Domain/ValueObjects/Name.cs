using Backend.Modules.Users.Domain.Exceptions;

namespace Backend.Modules.Users.Domain.ValueObjects;

public sealed record Name
{
    public string Value { get; }

    private Name(string value)
    {
        Value = value;
    }

    public static Name Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidNameError();

        value = value.Trim();

        if (value.Length < 2)
            throw new InvalidNameError();

        return new Name(value);
    }

    public override string ToString() => Value;
}
