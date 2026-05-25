using SmartHouseManagment.Domain.Entities;
using SmartHouseManagment.Domain.Spec;

namespace SmartHouseManagment.AppCore.Services.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> FindOneAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken);
    Task<List<TEntity>> FindAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}

public interface IRepository
{
    public IRepository<TEntity> Entity<TEntity>() where TEntity : BaseEntity;
}