using Backend.Modules.Absences.Application.Exceptions;
using Backend.Modules.Absences.Application.Ports;
using Backend.Shared;

namespace Backend.Modules.Absences.Application.UseCases.Cancel;

public sealed class CancelAbsenceHandler
{
    private readonly IAbsenceRepository _repo;
    private readonly IUnitOfWork _uow;

    public CancelAbsenceHandler(
        IAbsenceRepository repo,
        IUnitOfWork uow
    )
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<CancelAbsenceResult> Handle(
        CancelAbsenceCommand command,
        CancellationToken ct
    )
    {
        return await _uow.ExecuteAsync(async () =>
        {
            var absence = await _repo.GetByIdAsync(command.Id, ct);

            if (absence == null)
                throw new AbsenceNotFoundException();

            if (absence.UserId != command.CurrentUserId)
                throw new CannotCancelOtherUsersAbsenceException();

            absence.Cancel();

            return new CancelAbsenceResult(
                absence.Id,
                absence.UserId,
                absence.StartDate,
                absence.EndDate,
                (int)absence.Status
            );
        }, ct);
    }
}
