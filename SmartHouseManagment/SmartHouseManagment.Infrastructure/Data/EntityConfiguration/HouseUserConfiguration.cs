using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHouseManagment.Domain.Entities;

namespace SmartHouseManagment.Infrastructure.Data.EntityConfiguration;

public class HouseUserConfiguration : IEntityTypeConfiguration<HouseUser>
{
    public void Configure(EntityTypeBuilder<HouseUser> builder)
    {
        builder
            .ToTable("HouseUsers");

        builder
            .HasKey(x => new { x.HouseId, x.UserId });

        builder
            .HasOne(x => x.House)
            .WithMany(x => x.HouseUsers)
            .HasForeignKey(x => x.HouseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.HouseUsers)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.AdminId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
