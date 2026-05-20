using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHouseManagment.Domain.Entities;

namespace SmartHouseManagment.Infrastructure.Data.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder
            .Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.PasswordHash)
            .HasMaxLength(-1);

        builder
            .Property(x => x.Role)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);
    }
}