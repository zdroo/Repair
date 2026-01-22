using Repair.Domain.Enums;

namespace Repair.Domain.Events
{
    public record RepairStatusChangedDomainEvent(
    Guid repairRequestId,
    RepairStatus oldStatus,
    RepairStatus newStatus,
    DateTime occurredOn
);
}
