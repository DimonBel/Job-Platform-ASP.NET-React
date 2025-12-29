using CareerConnect.Domain.Entities;

namespace CareerConnect.Domain.Interfaces;

public interface IApplicationRepository : IRepository<Application>
{
    Task<IReadOnlyList<Application>> GetByJobIdAsync(int jobId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Application>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<Application?> GetByJobAndUserAsync(int jobId, int userId, CancellationToken cancellationToken = default);
    Task<bool> HasUserAppliedAsync(int jobId, int userId, CancellationToken cancellationToken = default);
}
