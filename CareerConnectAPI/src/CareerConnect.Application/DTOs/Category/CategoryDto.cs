namespace CareerConnect.Application.DTOs.Category;

public record CategoryDto
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string? Icon { get; init; }
    public string? Color { get; init; }
    public int Count { get; init; }
}
