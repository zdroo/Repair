using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repair.Infrastructure.Outbox;

namespace Repair.Infrastructure.Configurations
{
    public class OutboxMessageConfiguration
        : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("OutboxMessages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
       .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.Content)
                .IsRequired();

            builder.Property(x => x.OccurredOn)
                .IsRequired();
        }
    }
}
