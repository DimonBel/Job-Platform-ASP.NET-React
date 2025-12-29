namespace CareerConnect.Application.DTOs.Job;

public record JobDetailDto
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Company { get; init; } = string.Empty;
    public int CompanyId { get; init; }
    public string? CompanyLogo { get; init; }
    public string? CompanyDescription { get; init; }
    public string? CompanySize { get; init; }
    public string? CompanyWebsite { get; init; }
    public string Location { get; init; } = string.Empty;
    public string Salary { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public string ExperienceLevel { get; init; } = string.Empty;
    public string PostedAt { get; init; } = string.Empty;
    public DateTime? ClosingDate { get; init; }
    public bool Featured { get; init; }
    public int Applicants { get; init; }
    public string? Category { get; init; }
    public IReadOnlyList<string> Tags { get; init; } = new List<string>();
    public IReadOnlyList<string> Responsibilities { get; init; } = new List<string>();
    public IReadOnlyList<string> Requirements { get; init; } = new List<string>();
    public IReadOnlyList<string> Benefits { get; init; } = new List<string>();
}
