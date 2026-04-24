using Backend.Exceptions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Backend.Shared;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;

    public UnitOfWork(AppDbContext db)
    {
        _db = db;
    }

    public async Task<T> ExecuteAsync<T>(
        Func<Task<T>> action,
        CancellationToken ct
    )
    {
        await using var transaction =
            await _db.Database.BeginTransactionAsync(ct);

        try
        {
            var result = await action();

            await _db.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);

            return result;
        }
        catch (DomainException)
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
        catch (AppException)
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(ct);
            throw new UnitOfWorkFailedException(ex);
        }
    }

    public async Task ExecuteAsync(
    Func<Task> action,
    CancellationToken ct)
    {
        await using var transaction =
            await _db.Database.BeginTransactionAsync(ct);

        try
        {
            await action();

            await _db.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);
        }
        catch (DomainException)
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
        catch (AppException)
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(ct);
            throw new UnitOfWorkFailedException(ex);
        }
    }
}
