namespace CareerConnect.Application.DTOs.Auth;

public record UpdateProfileDto
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string? Phone { get; init; }
}
