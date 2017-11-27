namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class DSFamilyTempSensorSetUnitOfMeasurementCompletedModel
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

        public UnitOfMeasurementEnum UnitOfMeasurementId
        {
            get; set;
        }

        #endregion Properties
    }
}