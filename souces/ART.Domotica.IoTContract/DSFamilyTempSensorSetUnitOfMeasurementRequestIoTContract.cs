namespace ART.Domotica.IoTContract
{
    using System;

    using ART.Domotica.Enums;

    public class DSFamilyTempSensorSetUnitOfMeasurementRequestIoTContract
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