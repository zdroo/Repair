using Repair.Domain.Enums;

namespace Repair.Domain.Devices
{
    public class Phone : Device
    {
        public string IMEI { get; private set; } = null!;

        private Phone() { } //needed for EF

        public Phone(string imei, string model)
        {
            DeviceModel = model;
            IMEI = imei;
        }
    }
}
