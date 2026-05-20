using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartHouseManagment.Domain.Entities;

namespace SmartHouseManagment.Infrastructure.Repositories;

public class AppRepository<TEntity> : RepositoryBase<AppDbContext, TEntity> 
    where TEntity: BaseEntity 
{
    public AppRepository(
        AppDbContext context,
        ILogger<AppRepository<TEntity>> logger)
        : base(context, logger) { }
}