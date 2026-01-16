using Repair.Domain.Enums;

namespace Repair.Contracts.Repairs.CreateRepairRequest
{
    public class CreateRepairRequestRequest
    {
        public string PhoneModel { get; set; } = null!;
        public string IMEI { get; set; } = null!;
        public string ClientContact { get; set; } = null!;
        public string Country { get; set; } = null!;
        public IssueType IssueType { get; set; }
    }
}
