namespace CareerConnect.Application.DTOs.Job;

public record JobSearchParams
{
    public string? Query { get; init; }
    public string? Location { get; init; }
    public string? JobType { get; init; }
    public int? CategoryId { get; init; }
    public int? CompanyId { get; init; }
    public decimal? MinSalary { get; init; }
    public decimal? MaxSalary { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
