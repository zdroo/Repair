using Repair.Domain.Repairs;

namespace Repair.Application.Persistence
{
    public interface IRepairRequestRepository
    {
        Task AddAsync(RepairRequest repairRequest, CancellationToken cancellationToken);
        Task<RepairRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
