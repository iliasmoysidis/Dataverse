using Backend.Modules.Absences.Application.UseCases.Search;

namespace Backend.Modules.Absences.Application.Ports;

public interface IAbsenceQueries
{
    Task<PagedResult<GetAbsenceResult>> SearchAsync(SearchAbsencesQuery query, CancellationToken ct);
}
