namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums.SI;

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

        public SensorTriggerDetailModel HighAlarm
        {
            get; set;
        }

        public string Label
        {
            get; set;
        }

        public SensorTriggerDetailModel LowAlarm
        {
            get; set;
        }

        public SensorChartLimiterDetailModel SensorChartLimiter
        {
            get; set;
        }

        public byte SensorRangeId
        {
            get; set;
        }

        public UnitMeasurementEnum UnitMeasurementId
        {
            get; set;
        }

        #endregion Properties
    }
}