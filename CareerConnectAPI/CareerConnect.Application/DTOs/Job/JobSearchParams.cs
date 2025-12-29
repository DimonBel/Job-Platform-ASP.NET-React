namespace CareerConnect.Application.DTOs.Job;

public record JobSearchParams
{
    public string? Query { get; init; }
    public string? Location { get; init; }
    public string[]? Locations { get; init; }
    public string? JobType { get; init; }
    public string[]? JobTypes { get; init; }
    public string[]? ExperienceLevels { get; init; }
    public int? CategoryId { get; init; }
    public int? CompanyId { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
