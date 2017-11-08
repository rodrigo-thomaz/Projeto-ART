using System;

namespace ART.Domotica.Contract
{
    public class DSFamilyTempSensorGetAllByHardwareInApplicationIdResponseContract
    {
        public Guid DSFamilyTempSensorId { get; set; }

        public string DeviceAddress
        {
            get; set;
        }

        public byte DSFamilyTempSensorResolutionBits
        {
            get; set;
        }

        public string Family
        {
            get; set;
        }

        public decimal HighAlarm
        {
            get; set;
        }

        public decimal LowAlarm
        {
            get; set;
        }

        public byte TemperatureScaleId
        {
            get; set;
        }
    }
}
