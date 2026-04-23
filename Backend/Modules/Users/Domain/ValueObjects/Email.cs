using System.Net.Mail;
using Backend.Modules.Users.Domain.Exceptions;

namespace Backend.Modules.Users.Domain.ValueObjects;

public sealed record Email
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidEmailException();

        value = value.Trim().ToLower();

        try
        {
            var mail = new MailAddress(value);
            return new Email(mail.Address);
        } catch
        {
            throw new InvalidEmailException();
        }
    }

    public override string ToString()
        => Value;
}
