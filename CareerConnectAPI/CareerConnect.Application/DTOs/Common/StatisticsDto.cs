namespace CareerConnect.Application.DTOs.Common;

public record StatisticsDto
{
    public int ActiveJobs { get; init; }
    public int Companies { get; init; }
    public int JobSeekers { get; init; }
    public int Countries { get; init; }
}
