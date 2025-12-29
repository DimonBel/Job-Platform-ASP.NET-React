using CareerConnect.Domain.Common;

namespace CareerConnect.Domain.Entities;

public class Application : BaseEntity
{
    public string Status { get; set; } = "Pending"; // Pending, Reviewed, Shortlisted, Rejected, Hired
    public string? CoverLetter { get; set; }
    public string? ResumeUrl { get; set; }
    public DateTime AppliedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ReviewedDate { get; set; }
    public string? Notes { get; set; }
    
    // Foreign Keys
    public int JobId { get; set; }
    public int UserId { get; set; }
    
    // Navigation Properties
    public Job Job { get; set; } = null!;
    public User User { get; set; } = null!;
}
