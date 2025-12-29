using CareerConnect.Domain.Common;

namespace CareerConnect.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public string? Color { get; set; }
    
    // Navigation Properties
    public ICollection<Job> Jobs { get; set; } = new List<Job>();
}
