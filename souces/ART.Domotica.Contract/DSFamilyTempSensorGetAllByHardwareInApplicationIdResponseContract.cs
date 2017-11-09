using System;

namespace ART.Domotica.Contract
{
    public class DSFamilyTempSensorGetAllByHardwareInApplicationIdResponseContract
    {
        public Guid DSFamilyTempSensorId { get; set; }

        public short[] DeviceAddress
        {
            get; set;
        }

        public byte ResolutionBits
        {
            get; set;
        }

        public string Family
        {
            get; set;
        }

        public bool HasAlarm
        {
            get; set;
        }

        public decimal? HighAlarm
        {
            get; set;
        }

        public decimal? LowAlarm
        {
            get; set;
        }

        public byte TemperatureScaleId
        {
            get; set;
        }
    }
}
