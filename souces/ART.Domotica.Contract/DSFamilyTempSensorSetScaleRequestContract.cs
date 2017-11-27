namespace ART.Domotica.Contract
{
    using ART.Domotica.Enums;
    using System;

    public class DSFamilyTempSensorSetScaleRequestContract
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