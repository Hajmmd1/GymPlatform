namespace GymPlatform.SharedKernel;

public record PagedResult<T>(IReadOnlyList<T> Items, int PageNumber, int PageSize, int TotalCount)
{
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasNextPage => PageNumber < TotalPages;
    public bool HasPreviousPage => PageNumber > 1;
}

public record PagedRequest(int PageNumber = 1, int PageSize = 20)
{
    public int Skip => (PageNumber - 1) * PageSize;
    public int Take => PageSize;
}

public interface IPagedQueryHandler<TRequest, TResponse>
    where TRequest : PagedRequest
{
    Task<PagedResult<TResponse>> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}