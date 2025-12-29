using CareerConnect.Application.DTOs.Common;
using CareerConnect.Application.Interfaces;
using CareerConnect.Domain.Interfaces;

namespace CareerConnect.Application.Services;

public class StatisticsService : IStatisticsService
{
    private readonly IUnitOfWork _unitOfWork;

    public StatisticsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<StatisticsDto> GetStatisticsAsync(CancellationToken cancellationToken = default)
    {
        var totalJobs = await _unitOfWork.Jobs.CountAsync(j => j.IsActive, cancellationToken);
        var totalCompanies = await _unitOfWork.Companies.CountAsync(cancellationToken: cancellationToken);
        var totalCandidates = await _unitOfWork.Users.CountAsync(u => u.Role == "JobSeeker", cancellationToken);
        var verifiedCompanies = await _unitOfWork.Companies.CountAsync(c => c.IsVerified, cancellationToken);

        return new StatisticsDto
        {
            ActiveJobs = totalJobs,
            Companies = totalCompanies,
            JobSeekers = totalCandidates > 0 ? totalCandidates : 15000, // Default value for demo
            Countries = verifiedCompanies > 0 ? verifiedCompanies : 50 // Default value for demo
        };
    }
}
