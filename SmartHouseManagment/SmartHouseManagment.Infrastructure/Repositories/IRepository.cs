using SmartHouseManagment.Domain.Entities;
using SmartHouseManagment.Infrastructure.Specification;

namespace SmartHouseManagment.Infrastructure.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> FindByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<TEntity> FindOneAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken);
    Task<List<TEntity>> FindAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
}

public interface IRepository
{
    public IRepository<TEntity> Entity<TEntity>() where TEntity : BaseEntity;
}