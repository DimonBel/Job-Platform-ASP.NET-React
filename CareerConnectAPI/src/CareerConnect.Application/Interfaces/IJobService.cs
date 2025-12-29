using CareerConnect.Application.DTOs.Common;
using CareerConnect.Application.DTOs.Job;

namespace CareerConnect.Application.Interfaces;

public interface IJobService
{
    Task<JobDetailDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PagedResult<JobDto>> SearchAsync(JobSearchParams searchParams, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<JobDto>> GetFeaturedAsync(int count = 6, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<JobDto>> GetByCompanyAsync(int companyId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<JobDto>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<JobDto> CreateAsync(CreateJobDto dto, CancellationToken cancellationToken = default);
    Task<JobDto> UpdateAsync(int id, UpdateJobDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
