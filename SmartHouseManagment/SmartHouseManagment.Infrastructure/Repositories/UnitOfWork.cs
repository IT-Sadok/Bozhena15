using Microsoft.Extensions.Logging;
using SmartHouseManagment.AppCore.Configurations;
using SmartHouseManagment.Domain.Entities;

namespace SmartHouseManagment.Infrastructure.Repositories;

public class UnitOfWork(
    AppDbContext contextDb,
    ILogger<UnitOfWork> logger) : IUnitOfWork
{
    public IRepository<TEntity> Entity<TEntity>() where TEntity : class, IEntity
        => new AppRepository<TEntity>(contextDb);

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            var result = await contextDb.SaveChangesAsync(cancellationToken);
            return result > 0;

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while saving changes.");
            return false;   
        }
    }
}