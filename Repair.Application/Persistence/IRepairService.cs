using Repair.Domain.Enums;
using Repair.Domain.Repairs;

namespace Repair.Application.Persistence
{
    public interface IRepairService
    {
        Task<Guid> CreatePhoneRepairAsync(
            string phoneModel,
            string imei,
            string clientContact,
            string country,
            IssueType issueType,
            CancellationToken cancellationToken);

        Task UpdateRepairStatusAsync(
            Guid repairRequestId,
            RepairStatus newStatus,
            string? notes,
            CancellationToken cancellationToken);

        Task<RepairRequest?> GetRepairRequestAsync(
            Guid repairRequestId,
            CancellationToken cancellationToken);
    }
}
//Methods represent business actions

//No DTOs

//No EF concepts
