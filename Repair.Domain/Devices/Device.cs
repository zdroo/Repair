using Repair.Domain.Common;
using Repair.Domain.Enums;

namespace Repair.Domain.Devices
{
    public abstract class Device : BaseEntity
    {
        public string DeviceModel { get; protected set; }

        public void PowerOn() { } //wrong to put it here. Maybe a printer comes in -> should have interface IHasBattery containing this method and Phone/Laptop implement it
    }
}
