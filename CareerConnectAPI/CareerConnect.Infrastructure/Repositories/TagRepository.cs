using CareerConnect.Domain.Entities;
using CareerConnect.Domain.Interfaces;
using CareerConnect.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CareerConnect.Infrastructure.Repositories;

public class TagRepository : Repository<Tag>, ITagRepository
{
    public TagRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Tag?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower(), cancellationToken);
    }

    public async Task<IReadOnlyList<Tag>> GetPopularTagsAsync(int count, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(t => t.JobTags)
            .OrderByDescending(t => t.JobTags.Count)
            .Take(count)
            .ToListAsync(cancellationToken);
    }
}
