using Repair.Domain.Enums;

namespace Repair.Contracts.Repairs.RepairDetails
{
    public class RepairDetailsResponse
    {
        public Guid RepairRequestId { get; set; }

        public string DeviceModel { get; set; } = null!;
        public string DeviceType { get; set; } = null!;

        public string ClientContact { get; set; } = null!;
        public string Country { get; set; } = null!;

        public IssueType IssueType { get; set; }
        public RepairStatus CurrentStatus { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public IReadOnlyCollection<RepairPhaseHistoryDto> PhaseHistory { get; set; }
            = Array.Empty<RepairPhaseHistoryDto>();
    }
}
