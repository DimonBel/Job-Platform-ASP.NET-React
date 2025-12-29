using CareerConnect.Domain.Interfaces;
using CareerConnect.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ApplicationEntity = CareerConnect.Domain.Entities.Application;

namespace CareerConnect.Infrastructure.Repositories;

public class ApplicationRepository : Repository<ApplicationEntity>, IApplicationRepository
{
    public ApplicationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<ApplicationEntity>> GetByJobIdAsync(int jobId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(a => a.User)
            .Where(a => a.JobId == jobId)
            .OrderByDescending(a => a.AppliedDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ApplicationEntity>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(a => a.Job)
                .ThenInclude(j => j.Company)
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.AppliedDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<ApplicationEntity?> GetByJobAndUserAsync(int jobId, int userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(a => a.JobId == jobId && a.UserId == userId, cancellationToken);
    }

    public async Task<bool> HasUserAppliedAsync(int jobId, int userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(a => a.JobId == jobId && a.UserId == userId, cancellationToken);
    }
}
