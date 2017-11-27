namespace ART.Domotica.Model
{
    using ART.Domotica.Enums;
    using System;

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