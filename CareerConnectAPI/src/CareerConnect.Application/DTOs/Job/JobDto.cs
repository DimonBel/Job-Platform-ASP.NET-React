namespace CareerConnect.Application.DTOs.Job;

public record JobDto
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Company { get; init; } = string.Empty;
    public string? CompanyLogo { get; init; }
    public int CompanyId { get; init; }
    public string Location { get; init; } = string.Empty;
    public string Salary { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public string PostedAt { get; init; } = string.Empty;
    public bool Featured { get; init; }
    public IReadOnlyList<string> Tags { get; init; } = new List<string>();
}
