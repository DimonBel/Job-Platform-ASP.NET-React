using CareerConnect.Domain.Entities;

namespace CareerConnect.Domain.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<IReadOnlyList<Category>> GetCategoriesWithJobCountAsync(CancellationToken cancellationToken = default);
}
