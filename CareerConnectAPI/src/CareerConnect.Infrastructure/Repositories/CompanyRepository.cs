using CareerConnect.Domain.Entities;
using CareerConnect.Domain.Interfaces;
using CareerConnect.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CareerConnect.Infrastructure.Repositories;

public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    public CompanyRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Company?> GetByIdWithJobsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(c => c.Jobs.Where(j => j.IsActive))
                .ThenInclude(j => j.JobTags)
                    .ThenInclude(jt => jt.Tag)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<(IReadOnlyList<Company> Companies, int TotalCount)> SearchCompaniesAsync(
        string? query = null,
        string? industry = null,
        string? location = null,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var queryable = _dbSet
            .Include(c => c.Jobs.Where(j => j.IsActive))
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query))
        {
            query = query.ToLower();
            queryable = queryable.Where(c =>
                c.Name.ToLower().Contains(query) ||
                c.Description.ToLower().Contains(query) ||
                c.Industry.ToLower().Contains(query));
        }

        if (!string.IsNullOrWhiteSpace(industry))
        {
            queryable = queryable.Where(c => c.Industry == industry);
        }

        if (!string.IsNullOrWhiteSpace(location))
        {
            location = location.ToLower();
            queryable = queryable.Where(c => c.Location.ToLower().Contains(location));
        }

        var totalCount = await queryable.CountAsync(cancellationToken);

        var companies = await queryable
            .OrderByDescending(c => c.IsVerified)
            .ThenByDescending(c => c.Rating)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (companies, totalCount);
    }

    public async Task<IReadOnlyList<Company>> GetTopCompaniesAsync(int count, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(c => c.Jobs.Where(j => j.IsActive))
            .OrderByDescending(c => c.IsVerified)
            .ThenByDescending(c => c.Rating)
            .ThenByDescending(c => c.Jobs.Count(j => j.IsActive))
            .Take(count)
            .ToListAsync(cancellationToken);
    }
}
