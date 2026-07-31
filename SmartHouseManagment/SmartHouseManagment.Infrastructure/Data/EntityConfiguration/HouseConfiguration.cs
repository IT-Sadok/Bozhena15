using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHouseManagment.Domain.Entities;

namespace SmartHouseManagment.Infrastructure.Data.EntityConfiguration;

public class HouseConfiguration : IEntityTypeConfiguration<House>
{
    public void Configure(EntityTypeBuilder<House> builder)
    {
        builder
            .ToTable("Houses");

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.ComplexProperty(x => x.Address, ConfigureAddress);

        builder
            .HasMany(x => x.HouseUsers)
            .WithOne(x => x.House)
            .HasForeignKey(x => x.HouseId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private void ConfigureAddress(ComplexPropertyBuilder<Address> builder)
    {
        builder
            .Property(x => x.Address1)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(x => x.Address2)
            .HasMaxLength(100);

        builder
            .Property(x => x.City)
            .IsRequired()
            .HasMaxLength(30);

        builder
            .Property(x => x.State)
            .HasMaxLength(30);

        builder
            .Property(x => x.ZipCode)
            .IsRequired()
            .HasMaxLength(10);

        builder
            .Property(x => x.Country)
            .IsRequired()
            .HasMaxLength(50);
    }
}
