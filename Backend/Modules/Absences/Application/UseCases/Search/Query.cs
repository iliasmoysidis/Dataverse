namespace Backend.Modules.Absences.Application.UseCases.Search;

public sealed record SearchAbsencesQuery(
    string? Search,
    int? Status,
    int? UserId,
    DateOnly? From,
    DateOnly? To,
    string? SortBy,
    bool Desc,
    int Page = 1,
    int Limit = 20
);


