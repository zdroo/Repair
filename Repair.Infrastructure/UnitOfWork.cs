using Repair.Application.Interfaces;
using Repair.Infrastructure.Data;

namespace Repair.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly RepairDbContext _context;

    public UnitOfWork(RepairDbContext context)
    {
        _context = context;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
