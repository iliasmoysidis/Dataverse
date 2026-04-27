using Backend.Exceptions;
using Backend.Modules.Absences.Application.Ports;
using Backend.Modules.Absences.Domain;
using Backend.Infrastructure.UnitOfWork;

namespace Backend.Modules.Absences.Application.UseCases.Create;

public sealed class CreateAbsenceHandler
{
    private readonly IAbsenceRepository _repo;
    private readonly IUnitOfWork _uow;

    public CreateAbsenceHandler(IAbsenceRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<CreateAbsenceResult> Handle(CreateAbsenceCommand command, CancellationToken ct)
    {
        return await _uow.ExecuteAsync(async () =>
        {

            var overlap = await _repo.ExistsOverlappingAsync(
                command.UserId,
                command.StartDate,
                command.EndDate,
                ct
            );

            if (overlap)
                throw new OverlappingAbsenceException();

            var absence = Absence.Create(
                command.UserId,
                command.StartDate,
                command.EndDate
            );

            await _repo.AddAsync(absence, ct);

            return new CreateAbsenceResult(
                absence.Id,
                absence.UserId,
                absence.StartDate,
                absence.EndDate,
                (int)absence.Status
            );
        }, ct);
    }
}
