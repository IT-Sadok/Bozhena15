using Microsoft.Extensions.Logging;
using SmartHouseManagment.AppCore.Configurations;
using SmartHouseManagment.Domain.Entities;

namespace SmartHouseManagment.Infrastructure.Repositories;

public class Repository(
    AppDbContext contextDb) : IRepository
{
    public IRepository<TEntity> Entity<TEntity>() where TEntity : class, IEntity
        => new AppRepository<TEntity>(contextDb);
}