using CareerConnect.Domain.Entities;

namespace CareerConnect.Domain.Interfaces;

public interface ICompanyRepository : IRepository<Company>
{
    Task<Company?> GetByIdWithJobsAsync(int id, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<Company> Companies, int TotalCount)> SearchCompaniesAsync(
        string? query = null,
        string? industry = null,
        string? location = null,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Company>> GetTopCompaniesAsync(int count, CancellationToken cancellationToken = default);
}
