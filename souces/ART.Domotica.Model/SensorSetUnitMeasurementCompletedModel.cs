namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums.SI;

    public class SensorSetUnitMeasurementCompletedModel
    {
        #region Properties

        public Guid DeviceId
        {
            get; set;
        }

        public Guid DSFamilyTempSensorId
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