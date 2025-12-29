using CareerConnect.Domain.Entities;
using CareerConnect.Domain.Interfaces;
using CareerConnect.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CareerConnect.Infrastructure.Repositories;

public class JobRepository : Repository<Job>, IJobRepository
{
    public JobRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Job?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(j => j.Company)
            .Include(j => j.Category)
            .Include(j => j.JobTags)
                .ThenInclude(jt => jt.Tag)
            .Include(j => j.Responsibilities.OrderBy(r => r.Order))
            .Include(j => j.Requirements.OrderBy(r => r.Order))
            .Include(j => j.Benefits.OrderBy(b => b.Order))
            .Include(j => j.Applications)
            .FirstOrDefaultAsync(j => j.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Job>> GetFeaturedJobsAsync(int count, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(j => j.Company)
            .Include(j => j.JobTags)
                .ThenInclude(jt => jt.Tag)
            .Where(j => j.IsFeatured && j.IsActive)
            .OrderByDescending(j => j.PostedDate)
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<Job> Jobs, int TotalCount)> SearchJobsAsync(
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
        CancellationToken cancellationToken = default)
    {
        var queryable = _dbSet
            .Include(j => j.Company)
            .Include(j => j.JobTags)
                .ThenInclude(jt => jt.Tag)
            .Where(j => j.IsActive)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query))
        {
            query = query.ToLower();
            queryable = queryable.Where(j =>
                j.Title.ToLower().Contains(query) ||
                j.Description.ToLower().Contains(query) ||
                j.Company.Name.ToLower().Contains(query) ||
                j.JobTags.Any(jt => jt.Tag.Name.ToLower().Contains(query)));
        }

        // Filter by location - combine single location and multiple locations with OR logic
        var hasLocationFilter = !string.IsNullOrWhiteSpace(location);
        var hasLocationsFilter = locations != null && locations.Length > 0;

        if (hasLocationFilter || hasLocationsFilter)
        {
            queryable = queryable.Where(j =>
                (hasLocationFilter && j.Location.ToLower().Contains(location!.ToLower())) ||
                (hasLocationsFilter && locations!.Any(loc => j.Location.ToLower().Contains(loc.ToLower())))
            );
        }

        if (!string.IsNullOrWhiteSpace(jobType))
        {
            queryable = queryable.Where(j => j.JobType == jobType);
        }

        // Filter by multiple job types
        if (jobTypes != null && jobTypes.Length > 0)
        {
            queryable = queryable.Where(j => jobTypes.Contains(j.JobType));
        }

        // Filter by multiple experience levels
        if (experienceLevels != null && experienceLevels.Length > 0)
        {
            queryable = queryable.Where(j => experienceLevels.Contains(j.ExperienceLevel));
        }

        if (categoryId.HasValue)
        {
            queryable = queryable.Where(j => j.CategoryId == categoryId.Value);
        }

        if (companyId.HasValue)
        {
            queryable = queryable.Where(j => j.CompanyId == companyId.Value);
        }

        var totalCount = await queryable.CountAsync(cancellationToken);

        var jobs = await queryable
            .OrderByDescending(j => j.IsFeatured)
            .ThenByDescending(j => j.PostedDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (jobs, totalCount);
    }

    public async Task<IReadOnlyList<Job>> GetJobsByCompanyAsync(int companyId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(j => j.Company)
            .Include(j => j.JobTags)
                .ThenInclude(jt => jt.Tag)
            .Where(j => j.CompanyId == companyId && j.IsActive)
            .OrderByDescending(j => j.PostedDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Job>> GetJobsByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(j => j.Company)
            .Include(j => j.JobTags)
                .ThenInclude(jt => jt.Tag)
            .Where(j => j.CategoryId == categoryId && j.IsActive)
            .OrderByDescending(j => j.PostedDate)
            .ToListAsync(cancellationToken);
    }
}
