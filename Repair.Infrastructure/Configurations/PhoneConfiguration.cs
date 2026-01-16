using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repair.Domain.Devices;

namespace Repair.Infrastructure.Configurations;

public class PhoneConfiguration : IEntityTypeConfiguration<Phone>
{
    public void Configure(EntityTypeBuilder<Phone> builder)
    {
        builder.Property(p => p.IMEI)
            .IsRequired()
            .HasMaxLength(20);
    }
}
