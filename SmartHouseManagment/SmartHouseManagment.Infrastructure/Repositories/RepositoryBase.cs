using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartHouseManagment.Domain.Entities;
using SmartHouseManagment.Infrastructure.Specification;

namespace SmartHouseManagment.Infrastructure.Repositories;

public abstract class RepositoryBase<TDbContext, TEntity>(
    TDbContext context,
    ILogger<RepositoryBase<TDbContext, TEntity>> logger) : IRepository<TEntity>, IAsyncDisposable
    where TEntity : BaseEntity
    where TDbContext : DbContext
{
    public Task<TEntity> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> FindOneAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> FindAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public ValueTask DisposeAsync()
    {
        return context.DisposeAsync();
    }
}