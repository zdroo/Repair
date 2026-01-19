using Repair.Domain.Repairs;

namespace Repair.Application.Interfaces
{
    public interface IRepairRequestRepository
    {
        Task<IReadOnlyCollection<RepairRequest>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(RepairRequest repairRequest, CancellationToken cancellationToken);
        Task<RepairRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
