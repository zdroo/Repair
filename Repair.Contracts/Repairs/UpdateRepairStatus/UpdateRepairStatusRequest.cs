using Repair.Domain.Enums;

namespace Repair.Contracts.Repairs.UpdateRepairStatus
{
    public class UpdateRepairStatusRequest
    {
        public RepairStatus NewStatus { get; set; }
        public string? Notes { get; set; }
    }
}
