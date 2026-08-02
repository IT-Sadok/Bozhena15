using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHouseManagment.Domain.Entities;

namespace SmartHouseManagment.Infrastructure.Data.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("Users");
        
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(50);
        
        builder
            .Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .HasIndex(x => x.Email)
            .IsUnique();

        builder
            .HasMany(x => x.HouseUsers)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}