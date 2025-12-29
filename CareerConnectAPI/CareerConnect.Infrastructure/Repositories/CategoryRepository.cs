using CareerConnect.Domain.Entities;
using CareerConnect.Domain.Interfaces;
using CareerConnect.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CareerConnect.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Category>> GetCategoriesWithJobCountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(c => c.Jobs.Where(j => j.IsActive))
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);
    }
}
