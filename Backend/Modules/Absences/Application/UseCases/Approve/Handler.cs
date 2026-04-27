using Backend.Modules.Absences.Application.Exceptions;
using Backend.Modules.Absences.Application.Ports;
using Backend.Infrastructure.UnitOfWork;

namespace Backend.Modules.Absences.Application.UseCases.Approve;

public sealed class ApproveAbsenceHandler
{
    private readonly IAbsenceRepository _repo;
    private readonly IUnitOfWork _uow;

    public ApproveAbsenceHandler(IAbsenceRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<ApproveAbsenceResult> Handle(
        ApproveAbsenceCommand command,
        CancellationToken ct
    )
    {
        return await _uow.ExecuteAsync(async () =>
        {
            var absence = await _repo.GetByIdAsync(command.Id, ct);

            if (absence == null)
                throw new AbsenceNotFoundException();

            absence.Approve();

            return new ApproveAbsenceResult(
                absence.Id,
                absence.UserId,
                absence.StartDate,
                absence.EndDate,
                (int)absence.Status
            );

        }, ct);
    }
}
