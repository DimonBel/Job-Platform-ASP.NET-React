namespace CareerConnect.Application.DTOs.Company;

public record CompanySearchParams
{
    public string? Query { get; init; }
    public string? Industry { get; init; }
    public string? Location { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
