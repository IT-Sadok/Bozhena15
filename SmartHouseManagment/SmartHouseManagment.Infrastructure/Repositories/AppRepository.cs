using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartHouseManagment.Domain.Entities;

namespace SmartHouseManagment.Infrastructure.Repositories;

public class AppRepository<TEntity> : RepositoryBase<AppDbContext, TEntity> 
    where TEntity: class, IEntity 
{
    public AppRepository(AppDbContext context) : base(context) { }
}