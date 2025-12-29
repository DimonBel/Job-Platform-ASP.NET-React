using CareerConnect.Domain.Common;

namespace CareerConnect.Domain.Entities;

public class Tag : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    
    // Navigation Properties
    public ICollection<JobTag> JobTags { get; set; } = new List<JobTag>();
}
