using Repair.Domain.Common;
using Repair.Domain.Enums;

namespace Repair.Domain.Devices
{
    public abstract class Device : BaseEntity
    {
        public string DeviceModel { get; protected set; }
    }
}
