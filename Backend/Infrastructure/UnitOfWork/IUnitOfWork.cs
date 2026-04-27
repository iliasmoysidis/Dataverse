namespace Backend.Infrastructure.UnitOfWork;

public interface IUnitOfWork
{
    Task<T> ExecuteAsync<T>(
        Func<Task<T>> action,
        CancellationToken ct);
    Task ExecuteAsync(Func<Task> action, CancellationToken ct);
}
