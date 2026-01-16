using Microsoft.EntityFrameworkCore;
using Repair.Application.Persistence;
using Repair.Domain.Repairs;
using Repair.Infrastructure.Data;

namespace Repair.Infrastructure.Repositories;

public class RepairRequestRepository : IRepairRequestRepository
{
    private readonly RepairDbContext _context;

    public RepairRequestRepository(RepairDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(RepairRequest repairRequest, CancellationToken cancellationToken)
    {
        await _context.RepairRequests.AddAsync(repairRequest, cancellationToken);
    }

    public async Task<RepairRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.RepairRequests
            .Include(r => r.Device)
            .Include(r => r.PhaseHistory)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }
}
