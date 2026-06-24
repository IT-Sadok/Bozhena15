using SmartHouseManagment.AppCore.Configurations;
using SmartHouseManagment.Domain.Entities;

namespace SmartHouseManagment.Infrastructure.Repositories;

public class UnitOfWork(
    AppDbContext contextDb) : IUnitOfWork
{
    public IRepository<TEntity> Entity<TEntity>() where TEntity : class, IEntity
        => new AppRepository<TEntity>(contextDb);

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await contextDb.SaveChangesAsync(cancellationToken);
    }
}