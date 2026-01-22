using Microsoft.EntityFrameworkCore;
using Repair.Domain.Common;
using Repair.Domain.Devices;
using Repair.Domain.Repairs;
using Repair.Infrastructure.Outbox;
using System.Text.Json;

namespace Repair.Infrastructure.Data;

public class RepairDbContext : DbContext
{
    public RepairDbContext(DbContextOptions<RepairDbContext> options)
        : base(options)
    {
    }

    public DbSet<Device> Devices => Set<Device>();
    public DbSet<RepairRequest> RepairRequests => Set<RepairRequest>();
    public DbSet<RepairPhaseHistory> RepairPhaseHistories => Set<RepairPhaseHistory>();

    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepairDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(
    CancellationToken cancellationToken = default)
    {
        AddOutboxMessages();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void AddOutboxMessages()
    {
        var domainEvents = ChangeTracker
            .Entries<BaseEntity>()
            .SelectMany(e => e.Entity.DomainEvents)
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            OutboxMessages.Add(new OutboxMessage(
                DateTime.UtcNow,
                domainEvent.GetType().Name,
                JsonSerializer.Serialize(domainEvent, domainEvent.GetType())
            ));
        }

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            entry.Entity.ClearDomainEvents();
        }
    }
}
