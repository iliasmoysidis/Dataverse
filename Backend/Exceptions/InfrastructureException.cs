namespace Backend.Exceptions;

public abstract class InfrastructureException : Exception
{
    public string Code { get; }

    protected InfrastructureException(
        string code,
        string message,
        Exception? inner = null)
        : base(message, inner)
    {
        Code = code;
    }
}
