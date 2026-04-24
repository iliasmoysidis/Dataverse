using Backend.Modules.Absences.Application.Ports;

namespace Backend.Modules.Absences.Application.UseCases.Search;

public sealed class SearchAbsencesHandler
{
    private readonly IAbsenceQueries _queries;

    public SearchAbsencesHandler(
        IAbsenceQueries queries
    )
    {
        _queries = queries;
    }

    public Task<PagedResult<GetAbsenceResult>> Handle(
        SearchAbsencesQuery query,
        CancellationToken ct
    )
    {
        return _queries.SearchAsync(query, ct);
    }
}
