using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using SmartHouseManagment.AppCore.Services.Interfaces;
using SmartHouseManagment.Domain.Entities;
using SmartHouseManagment.Infrastructure.Repositories;

namespace SmartHouseManagment.Infrastructure.Extensions;

public static class AppExtensions
{
    private const int DatabaseRetryCount = 5;
    
    public static void AddCoreServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddPostgreSqlDbContext<AppDbContext>(configuration);
        services.AddRepositories();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
    }

    private static void AddPostgreSqlDbContext<T>(
        this IServiceCollection services,
        IConfiguration configuration)
        where T : DbContext
    {
        var connectionString = configuration.GetConnectionString("DatabaseConnection") ?? string.Empty;
        
        services.AddDbContext<T>(options =>
            options.UseNpgsql(connectionString, builder => 
                builder.ExecutionStrategy(x => 
                    new NpgsqlRetryingExecutionStrategy(x, DatabaseRetryCount))));
    }

    private static void AddRepositories(
        this IServiceCollection services)
    {
        var entityInfos = typeof(BaseEntity).Assembly.GetTypes()
            .Where(x => typeof(BaseEntity).IsAssignableFrom(x));
        
        foreach (var entityInfo in entityInfos)
        {
            services.TryAddScoped(
                typeof(IRepository<>).MakeGenericType(entityInfo),
                typeof(AppRepository<>).MakeGenericType(entityInfo));
            
            services.AddScoped(typeof(AppRepository<>).MakeGenericType(entityInfo));
        }
        
        services.AddScoped<IRepository, Repository>();
    }
}