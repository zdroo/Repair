using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repair.Domain.Repairs;

namespace Repair.Infrastructure.Configurations;

public class RepairPhaseHistoryConfiguration
    : IEntityTypeConfiguration<RepairPhaseHistory>
{
    public void Configure(EntityTypeBuilder<RepairPhaseHistory> builder)
    {
        builder.ToTable("RepairPhaseHistories");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.Status)
            .IsRequired();

        builder.Property(h => h.ChangedAt)
            .IsRequired();

        builder.Property(h => h.Notes)
            .HasMaxLength(500);
    }
}
