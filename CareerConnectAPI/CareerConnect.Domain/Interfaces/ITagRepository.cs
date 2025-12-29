using CareerConnect.Domain.Entities;

namespace CareerConnect.Domain.Interfaces;

public interface ITagRepository : IRepository<Tag>
{
    Task<Tag?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Tag>> GetPopularTagsAsync(int count, CancellationToken cancellationToken = default);
}
