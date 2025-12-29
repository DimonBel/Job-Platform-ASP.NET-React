using CareerConnect.Domain.Entities;

namespace CareerConnect.Domain.Interfaces;

public interface IJobRepository : IRepository<Job>
{
    Task<Job?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Job>> GetFeaturedJobsAsync(int count, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<Job> Jobs, int TotalCount)> SearchJobsAsync(
        string? query = null,
        string? location = null,
        string[]? locations = null,
        string? jobType = null,
        string[]? jobTypes = null,
        string[]? experienceLevels = null,
        int? categoryId = null,
        int? companyId = null,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Job>> GetJobsByCompanyAsync(int companyId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Job>> GetJobsByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
}
