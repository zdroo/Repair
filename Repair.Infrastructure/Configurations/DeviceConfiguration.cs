using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repair.Domain.Devices;

namespace Repair.Infrastructure.Configurations;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.ToTable("Devices");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.DeviceModel)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .HasDiscriminator<string>("DeviceType")
            .HasValue<Phone>("Phone");
    }
}
