using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repair.Domain.Repairs;

namespace Repair.Infrastructure.Configurations;

public class RepairRequestConfiguration : IEntityTypeConfiguration<RepairRequest>
{
    public void Configure(EntityTypeBuilder<RepairRequest> builder)
    {
        builder.ToTable("RepairRequests");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.ClientContact)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.Country)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.CurrentStatus)
            .IsRequired();

        builder.Property(r => r.StartDate)
            .IsRequired();

        builder.HasOne(r => r.Device)
            .WithMany()
            .HasForeignKey(r => r.DeviceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(r => r.PhaseHistory)
            .WithOne()
            .HasForeignKey("RepairRequestId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
