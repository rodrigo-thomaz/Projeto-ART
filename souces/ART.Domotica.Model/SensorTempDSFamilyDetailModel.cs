namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums.SI;

    public class SensorTempDSFamilyDetailModel
    {
        #region Properties

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

        public Guid SensorTempDSFamilyId
        {
            get; set;
        }

        public byte SensorTempDSFamilyResolutionId
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