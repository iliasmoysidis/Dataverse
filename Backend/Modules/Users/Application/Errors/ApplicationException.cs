public abstract class ApplicationLayerException : Exception
{
    public string Code { get; }

    protected ApplicationLayerException(string code, string message) : base(message)
    {
        Code = code;
    }
}
