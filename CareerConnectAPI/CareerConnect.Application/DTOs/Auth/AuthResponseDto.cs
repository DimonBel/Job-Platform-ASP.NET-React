namespace CareerConnect.Application.DTOs.Auth;

public record AuthResponseDto
{
    public bool Success { get; init; }
    public string? Token { get; init; }
    public DateTime? ExpiresAt { get; init; }
    public UserInfoDto? User { get; init; }
    public string? Error { get; init; }
}

public record UserInfoDto
{
    public int Id { get; init; }
    public string Email { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public string? ProfilePicture { get; init; }
    public int? CompanyId { get; init; }
}
