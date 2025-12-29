namespace CareerConnect.Application.DTOs.Company;

public record CompanyDetailDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Industry { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
    public string? Logo { get; init; }
    public string? Website { get; init; }
    public string? Size { get; init; }
    public string? FoundedYear { get; init; }
    public decimal? Rating { get; init; }
    public int ReviewCount { get; init; }
    public int OpenJobsCount { get; init; }
    public bool IsVerified { get; init; }
    public IReadOnlyList<DTOs.Job.JobDto> RecentJobs { get; init; } = new List<DTOs.Job.JobDto>();
}
