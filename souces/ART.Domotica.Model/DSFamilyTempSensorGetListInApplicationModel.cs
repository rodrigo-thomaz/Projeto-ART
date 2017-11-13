namespace ART.Domotica.Model
{
    using System;

    public class DSFamilyTempSensorGetListInApplicationModel
    {
        #region Properties

        public string DeviceAddress
        {
            get; set;
        }

        public Guid DSFamilyTempSensorId
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

        public Guid HardwareInApplicationId
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