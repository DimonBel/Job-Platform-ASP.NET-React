using CareerConnect.Domain.Common;

namespace CareerConnect.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Resume { get; set; }
    public string Role { get; set; } = "JobSeeker"; // JobSeeker, Employer, Admin
    public bool IsActive { get; set; } = true;
    public DateTime? LastLoginAt { get; set; }
    
    // Foreign Key (nullable for job seekers)
    public int? CompanyId { get; set; }
    
    // Navigation Properties
    public Company? Company { get; set; }
    public ICollection<Application> Applications { get; set; } = new List<Application>();
    
    public string FullName => $"{FirstName} {LastName}";
}
