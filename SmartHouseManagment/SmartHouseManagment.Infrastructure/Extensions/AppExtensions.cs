using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using SmartHouseManagment.AppCore.Behaviors;
using SmartHouseManagment.AppCore.Services.Interfaces;
using SmartHouseManagment.Domain.Entities;
using SmartHouseManagment.Infrastructure.Repositories;
using AppCoreAnchor = SmartHouseManagment.AppCore.Anchor;

namespace SmartHouseManagment.Infrastructure.Extensions;

public static class AppExtensions
{
    private const int DatabaseRetryCount = 5;
    
    public static void AddCoreServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AppCoreAnchor).Assembly))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        services.AddFluentValidators();
        
        services.AddPostgreSqlDbContext<AppDbContext>(configuration);
        services.AddRepositories();
        
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
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

    private static void AddFluentValidators(
        this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssembliesOf(typeof(AppCoreAnchor))
            .AddClasses(classes => 
                classes.AssignableTo(typeof(IValidator<>))
                    .Where(type => type.BaseType is not null
                    && type.BaseType.IsGenericType
                    && typeof(IBaseRequest).IsAssignableFrom(type.BaseType.GetGenericArguments()[0])),
                publicOnly: false)
            .AsImplementedInterfaces()
            .WithTransientLifetime());
    }
}