using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SmartHouseManagment.Domain.Entities;

namespace SmartHouseManagment.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly,
            type => type.Namespace?.Contains(nameof(Data.EntityConfiguration)) == true);
    }
}