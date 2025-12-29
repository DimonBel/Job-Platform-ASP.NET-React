using CareerConnect.Domain.Common;

namespace CareerConnect.Domain.Entities;

public class JobResponsibility : BaseEntity
{
    public string Description { get; set; } = string.Empty;
    public int Order { get; set; }
    
    // Foreign Key
    public int JobId { get; set; }
    
    // Navigation Property
    public Job Job { get; set; } = null!;
}
