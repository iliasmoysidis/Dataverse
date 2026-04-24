using Backend.Modules.Absences.Application.Ports;

namespace Backend.Modules.Absences.Application.UseCases.GetPending;

public sealed class GetAbsencesHandler
{
    private readonly IAbsenceQueries _queries;

    public GetAbsencesHandler(IAbsenceQueries queries)
    {
        _queries = queries;
    }

    public async Task<IReadOnlyCollection<GetAbsenceResult>> Handle(
        GetAbsencesQuery query,
        CancellationToken ct
    )
    {
        return await _queries.GetAbsencesAsync(ct);
    }
}
