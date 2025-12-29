namespace CareerConnect.Application.DTOs.Job;

public record CreateJobDto
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
    public string Salary { get; init; } = string.Empty;
    public string JobType { get; init; } = string.Empty;
    public string ExperienceLevel { get; init; } = string.Empty;
    public int CompanyId { get; init; }
    public int CategoryId { get; init; }
    public bool IsFeatured { get; init; }
    public DateTime? ClosingDate { get; init; }
    public IReadOnlyList<string> Tags { get; init; } = new List<string>();
    public IReadOnlyList<string> Responsibilities { get; init; } = new List<string>();
    public IReadOnlyList<string> Requirements { get; init; } = new List<string>();
    public IReadOnlyList<string> Benefits { get; init; } = new List<string>();
}
