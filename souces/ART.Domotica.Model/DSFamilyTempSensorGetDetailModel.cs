namespace ART.Domotica.Model
{
    using System;

    public class DSFamilyTempSensorGetDetailModel
    {
        #region Properties

        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public byte DSFamilyTempSensorResolutionId
        {
            get; set;
        }

        public bool HasHighAlarm { get; set; }

        public bool HasLowAlarm { get; set; }

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