using SmartHouseManagment.Domain.Entities;
using SmartHouseManagment.Domain.Spec;

namespace SmartHouseManagment.AppCore.Configurations;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity> FindOneAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken);
    Task<List<TEntity>> FindAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
}

public interface IUnitOfWork
{
    public IRepository<TEntity> Entity<TEntity>() where TEntity : class, IEntity;
    Task SaveChangesAsync(CancellationToken cancellationToken);
}