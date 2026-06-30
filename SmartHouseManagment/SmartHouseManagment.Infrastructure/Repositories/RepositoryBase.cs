using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartHouseManagment.AppCore.Configurations;
using SmartHouseManagment.Domain.Entities;
using SmartHouseManagment.Domain.Spec;
using SmartHouseManagment.Infrastructure.Extensions;

namespace SmartHouseManagment.Infrastructure.Repositories;

public abstract class RepositoryBase<TDbContext, TEntity>(
    TDbContext context) : IRepository<TEntity>, IAsyncDisposable
    where TEntity : class, IEntity
    where TDbContext : DbContext
{
    public async Task<TEntity> FindOneAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken)
    {
        var specResult = GetSpecQuery(context.Set<TEntity>(), spec);
        
        return await specResult.FirstOrDefaultAsync((cancellationToken));
    }

    public async Task<List<TEntity>> FindAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken)
    {
        var specResult = GetSpecQuery(context.Set<TEntity>(), spec);
        
        return await specResult.ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken)
        => (await FindOneAsync(spec, cancellationToken)) is not null;

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        return context.DisposeAsync();
    }

    private static IQueryable<TEntity> GetSpecQuery(
        IQueryable<TEntity> inputQuery,
        ISpecification<TEntity> spec)
    {
        var query = inputQuery;

        if (spec.Criterias?.Count > 0)
        {
            var exp = spec.Criterias[0];

            for (int i = 1; i < spec.Criterias.Count; i++)
                exp = exp.And(spec.Criterias[i]);
            
            query = query.Where(exp);
        }
        
        return query;
    }
}