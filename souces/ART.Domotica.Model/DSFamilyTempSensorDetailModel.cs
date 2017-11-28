namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class DSFamilyTempSensorDetailModel
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

        public decimal HighChartLimiterCelsius
        {
            get; set;
        }

        public string Label
        {
            get; set;
        }

        public TempSensorAlarmGetDetailModel LowAlarm
        {
            get; set;
        }

        public decimal LowChartLimiterCelsius
        {
            get; set;
        }

        public SensorRangeGetDetailModel SensorRange
        {
            get; set;
        }

        public UnitOfMeasurementEnum UnitOfMeasurementId
        {
            get; set;
        }

        #endregion Properties
    }
}