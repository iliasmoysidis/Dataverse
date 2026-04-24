namespace Backend.Exceptions;

public sealed class UnitOfWorkFailedException
    : InfrastructureException
{
    public UnitOfWorkFailedException(Exception inner)
        : base(
            "shared.unit_of_work_failed",
            "Database transaction failed.",
            inner)
    {
    }
}
