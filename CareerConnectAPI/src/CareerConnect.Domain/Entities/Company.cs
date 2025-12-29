using CareerConnect.Domain.Common;

namespace CareerConnect.Domain.Entities;

public class Company : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Industry { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string? Logo { get; set; }
    public string? Website { get; set; }
    public string? Size { get; set; }
    public string? FoundedYear { get; set; }
    public decimal? Rating { get; set; }
    public int ReviewCount { get; set; }
    public bool IsVerified { get; set; }
    
    // Navigation Properties
    public ICollection<Job> Jobs { get; set; } = new List<Job>();
}
