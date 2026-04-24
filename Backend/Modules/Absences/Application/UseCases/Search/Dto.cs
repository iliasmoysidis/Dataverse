public sealed record PagedResult<T>(
    IReadOnlyCollection<T> Items,
    int Page,
    int Limit,
    int TotalCount
)
{
    public int TotalPages =>
        TotalCount == 0
         ? 0
         : (int)Math.Ceiling(TotalCount / (double) Limit);

    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;
}

public sealed record GetAbsenceResult(
    int Id,
    DateOnly StartDate,
    DateOnly EndDate,
    int Status,
    UserInfoResult User
);

public sealed record UserInfoResult(
    int Id,
    string Email,
    string Name,
    string Surname,
    int Role
);
