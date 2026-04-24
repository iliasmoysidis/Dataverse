using Backend.Modules.Absences.Application.Ports;

namespace Backend.Modules.Absences.Application.UseCases.GetPendingByUser;

public sealed class GetPendingAbsencesByUserHandler
{
    private readonly IAbsenceQueries _queries;

    public GetPendingAbsencesByUserHandler(
        IAbsenceQueries queries
    )
    {
        _queries = queries;
    }

    public Task<IReadOnlyCollection<GetPendingAbsencesByUserResult>> Handle(
        GetPendingAbsencesByUserQuery query,
        CancellationToken ct
    )
    {
        return _queries.GetPendingByUserAsync(
            query.UserId,
            ct
        );
    }
}
