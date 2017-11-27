namespace ART.Domotica.IoTContract
{
    using ART.Domotica.Enums;
    using System;

    public class DSFamilyTempSensorSetScaleRequestIoTContract
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