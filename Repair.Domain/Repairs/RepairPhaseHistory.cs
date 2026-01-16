using Repair.Domain.Common;
using Repair.Domain.Enums;

namespace Repair.Domain.Repairs
{
    public class RepairPhaseHistory : BaseEntity
    {
        public RepairStatus Status { get; private set; }
        public DateTime ChangedAt { get; private set; }
        public string? Notes { get; private set; }

        private RepairPhaseHistory() { }

        public RepairPhaseHistory(RepairStatus status, string? notes = null)
        {
            Status = status;
            Notes = notes;
            ChangedAt = DateTime.UtcNow;
        }
    }
}
