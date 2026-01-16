using Microsoft.EntityFrameworkCore;
using Repair.Domain.Devices;
using Repair.Domain.Repairs;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepairDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
