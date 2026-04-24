using Backend.Modules.Absences.Application.Ports;

namespace Backend.Modules.Absences.Application.UseCases.GetPending;

public sealed record GetPendingAbsencesHandler
{
    private readonly IAbsenceQueries _queries;

    public GetPendingAbsencesHandler(IAbsenceQueries queries)
    {
        _queries = queries;
    }

    public async Task<IReadOnlyCollection<GetPendingAbsencesResult>> Handle(
        GetPendingAbsencesQuery query,
        CancellationToken ct
    )
    {
        return await _queries.GetPendingAsync(ct);
    }
}
