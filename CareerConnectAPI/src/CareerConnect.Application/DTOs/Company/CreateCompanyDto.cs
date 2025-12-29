namespace CareerConnect.Application.DTOs.Company;

public record CreateCompanyDto
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Industry { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
    public string? Logo { get; init; }
    public string? Website { get; init; }
    public string? Size { get; init; }
    public string? FoundedYear { get; init; }
}
