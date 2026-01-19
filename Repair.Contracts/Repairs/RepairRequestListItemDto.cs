namespace Repair.Contracts.Repairs
{
    public class RepairRequestListItemDto
    {
        public Guid RepairRequestId { get; set; }
        public string DeviceModel { get; set; } = null!;
        public string DeviceType { get; set; } = null!;
        public string ClientContact { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string CurrentStatus { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
