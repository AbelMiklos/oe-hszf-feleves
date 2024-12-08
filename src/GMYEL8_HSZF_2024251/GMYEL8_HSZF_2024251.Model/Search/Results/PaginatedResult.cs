namespace GMYEL8_HSZF_2024251.Model.Search.Results;

public class PaginatedResult<T>
{
	public List<T> Items { get; set; } = [];

	public int TotalCount { get; set; }

	public int CurrentPage { get; set; }

	public int PageSize { get; set; }

	public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
