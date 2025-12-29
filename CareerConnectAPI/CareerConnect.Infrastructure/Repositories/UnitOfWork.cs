using CareerConnect.Domain.Interfaces;
using CareerConnect.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace CareerConnect.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;

    private IJobRepository? _jobs;
    private ICompanyRepository? _companies;
    private ICategoryRepository? _categories;
    private ITagRepository? _tags;
    private IUserRepository? _users;
    private IApplicationRepository? _applications;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IJobRepository Jobs => _jobs ??= new JobRepository(_context);
    public ICompanyRepository Companies => _companies ??= new CompanyRepository(_context);
    public ICategoryRepository Categories => _categories ??= new CategoryRepository(_context);
    public ITagRepository Tags => _tags ??= new TagRepository(_context);
    public IUserRepository Users => _users ??= new UserRepository(_context);
    public IApplicationRepository Applications => _applications ??= new ApplicationRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
