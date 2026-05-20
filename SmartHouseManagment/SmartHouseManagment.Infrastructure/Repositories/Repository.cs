using Microsoft.Extensions.Logging;
using SmartHouseManagment.Domain.Entities;

namespace SmartHouseManagment.Infrastructure.Repositories;

public class Repository(
    AppDbContext contextDb,
    ILoggerFactory logger) : IRepository
{
    public IRepository<TEntity> Entity<TEntity>() where TEntity : BaseEntity
        => new AppRepository<TEntity>(contextDb, logger.CreateLogger<AppRepository<TEntity>>());
}