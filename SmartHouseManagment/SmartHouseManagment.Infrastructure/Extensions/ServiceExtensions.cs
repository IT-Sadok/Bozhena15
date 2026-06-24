using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using SmartHouseManagment.AppCore.Behaviors;
using SmartHouseManagment.AppCore.Configurations;
using SmartHouseManagment.AppCore.Services;
using SmartHouseManagment.AppCore.Services.Interfaces;
using SmartHouseManagment.Domain.Entities;
using SmartHouseManagment.Infrastructure.Repositories;
using AppCoreAnchor = SmartHouseManagment.AppCore.Anchor;

namespace SmartHouseManagment.Infrastructure.Extensions;

public static class ServiceExtensions
{
    private const int DatabaseRetryCount = 5;
    
    public static void AddCoreServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();
        services.AddProblemDetails();

        services.AddMediatR();
        
        services.AddFluentValidators();
        
        services.AddPostgreSqlDbContext<AppDbContext>(configuration);
        services.AddRepositories();
        
        services.AddServices();
        
        services.AddAuthorization(configuration);
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    private static void AddAuthorization(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddIdentityCore<User>(options =>
            {
                options.User.RequireUniqueEmail = true;
                
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Secret"] ?? string.Empty))
                };
            });
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
        var entityInfos = typeof(IEntity).Assembly.GetTypes()
            .Where(x => typeof(IEntity).IsAssignableFrom(x));
        
        foreach (var entityInfo in entityInfos)
        {
            services.TryAddScoped(
                typeof(IRepository<>).MakeGenericType(entityInfo),
                typeof(AppRepository<>).MakeGenericType(entityInfo));
            
            services.AddScoped(typeof(AppRepository<>).MakeGenericType(entityInfo));
        }
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
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

    private static void AddServices(
        this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
    }

    private static IServiceCollection AddMediatR(
         this IServiceCollection services)
    {
        var handlerAssembly = typeof(AppCoreAnchor).Assembly;

        var handlerType = typeof(IRequestHandler<,>);
        var handlers = handlerAssembly.GetTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == handlerType))
            .ToList();

        foreach (var handler in handlers)
        {
            var interfaces = handler.GetInterfaces();
            foreach (var i in interfaces)
            {
                services.AddScoped(i, handler);
            }
        }

        services.AddScoped<IMediator, Mediator>();
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}