using Backend.Modules.Users.Domain.Exceptions;

namespace Backend.Modules.Users.Domain.ValueObjects;

public sealed record Surname
{
    public string Value { get; }

    private Surname(string value)
    {
        Value = value;
    }

    public static Surname Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidSurnameError();

        value = value.Trim();

        if (value.Length < 2)
            throw new InvalidSurnameError();

        return new Surname(value);
    }

    public override string ToString() => Value;
}
