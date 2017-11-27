namespace ART.Domotica.Contract
{
    using System;

    using ART.Domotica.Enums;

    public class DSFamilyTempSensorSetUnitOfMeasurementRequestContract
    {
        #region Properties

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