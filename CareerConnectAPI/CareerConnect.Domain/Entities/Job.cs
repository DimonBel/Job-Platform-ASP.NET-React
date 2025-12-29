using CareerConnect.Domain.Common;

namespace CareerConnect.Domain.Entities;

public class Job : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Salary { get; set; } = string.Empty;
    public string JobType { get; set; } = string.Empty;
    public string ExperienceLevel { get; set; } = string.Empty;
    public bool IsFeatured { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime PostedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ClosingDate { get; set; }
    
    // Foreign Keys
    public int CompanyId { get; set; }
    public int CategoryId { get; set; }
    
    // Navigation Properties
    public Company Company { get; set; } = null!;
    public Category Category { get; set; } = null!;
    public ICollection<JobTag> JobTags { get; set; } = new List<JobTag>();
    public ICollection<JobResponsibility> Responsibilities { get; set; } = new List<JobResponsibility>();
    public ICollection<JobRequirement> Requirements { get; set; } = new List<JobRequirement>();
    public ICollection<JobBenefit> Benefits { get; set; } = new List<JobBenefit>();
    public ICollection<Application> Applications { get; set; } = new List<Application>();
}
