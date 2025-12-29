using System.ComponentModel.DataAnnotations;

namespace CareerConnect.Application.DTOs.Auth;

public record RegisterDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; init; } = string.Empty;
    
    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; init; } = string.Empty;
    
    [Required(ErrorMessage = "First name is required")]
    [MaxLength(100)]
    public string FirstName { get; init; } = string.Empty;
    
    [Required(ErrorMessage = "Last name is required")]
    [MaxLength(100)]
    public string LastName { get; init; } = string.Empty;
    
    [Phone(ErrorMessage = "Invalid phone number")]
    public string? Phone { get; init; }
    
    // Optional: For employer registration
    public int? CompanyId { get; init; }
}
