using Backend.Modules.Absences.Application.Ports;

namespace Backend.Modules.Absences.Application.UseCases.GetByUser;

public sealed class GetAbsencesByUserHandler
{
    private readonly IAbsenceQueries _queries;

    public GetAbsencesByUserHandler(
        IAbsenceQueries queries
    )
    {
        _queries = queries;
    }

    public Task<IReadOnlyCollection<GetAbsencesByUserResult>> Handle(
        GetAbsencesByUserQuery query,
        CancellationToken ct
    )
    {
        return _queries.GetByUserAsync(
            query.UserId,
            ct
        );
    }
}
