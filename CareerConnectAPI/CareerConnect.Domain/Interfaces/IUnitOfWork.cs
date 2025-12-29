namespace CareerConnect.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IJobRepository Jobs { get; }
    ICompanyRepository Companies { get; }
    ICategoryRepository Categories { get; }
    ITagRepository Tags { get; }
    IUserRepository Users { get; }
    IApplicationRepository Applications { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
