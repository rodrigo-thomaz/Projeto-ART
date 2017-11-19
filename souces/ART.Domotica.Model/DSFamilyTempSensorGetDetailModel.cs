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

        public TempSensorAlarmGetDetailModel HighAlarm
        {
            get; set;
        }

        public TempSensorAlarmGetDetailModel LowAlarm
        {
            get; set;
        }

        public byte TemperatureScaleId
        {
            get; set;
        }

        public TempSensorRangeGetDetailModel TempSensorRange
        {
            get; set;
        }

        public decimal HighChartLimiterCelsius
        {
            get; set;
        }

        public decimal LowChartLimiterCelsius
        {
            get; set;
        }

        #endregion Properties
    }
}