using Backend.Modules.Absences.Application.Exceptions;
using Backend.Modules.Absences.Application.Ports;
using Backend.Infrastructure.UnitOfWork;

namespace Backend.Modules.Absences.Application.UseCases.Reject;

public sealed class RejectAbsenceHandler
{
    private readonly IAbsenceRepository _repo;
    private readonly IUnitOfWork _uow;

    public RejectAbsenceHandler(
        IAbsenceRepository repo,
        IUnitOfWork uow
    )
    {
        _repo = repo;
        _uow = uow;
    }

    public Task<RejectAbsenceResult> Handle(
        RejectAbsenceCommand command,
        CancellationToken ct
    )
    {
        return _uow.ExecuteAsync(async () =>
        {
            var absence = await _repo.GetByIdAsync(command.Id, ct);

            if (absence == null)
                throw new AbsenceNotFoundException();

            absence.Reject();

            return new RejectAbsenceResult(
                absence.Id,
                absence.UserId,
                absence.StartDate,
                absence.EndDate,
                (int)absence.Status
            );
        }, ct);
    }
}
