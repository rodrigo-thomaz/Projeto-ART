using System;

namespace ART.Domotica.Model
{
    public class DSFamilyTempSensorGetListInApplicationModel
    {
        #region Properties

        public Guid DSFamilyTempSensorId { get; set; }

        public Guid HardwareInApplicationId { get; set; }

        public string DeviceAddress
        {
            get; set;
        }

        public byte DSFamilyTempSensorResolutionId
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

        #endregion Properties
    }
}