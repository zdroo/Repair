using Repair.Domain.Enums;

namespace Repair.Contracts.Repairs.RepairDetails
{
    public class RepairPhaseHistoryDto
    {
        public RepairStatus Status { get; set; }
        public DateTime ChangedAt { get; set; }
        public string? Notes { get; set; }
    }
}
