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

        public SensorUnitMeasurementScaleDetailModel SensorUnitMeasurementScale
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