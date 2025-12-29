namespace CareerConnect.Domain.Entities;

public class JobTag
{
    public int JobId { get; set; }
    public int TagId { get; set; }
    
    // Navigation Properties
    public Job Job { get; set; } = null!;
    public Tag Tag { get; set; } = null!;
}
