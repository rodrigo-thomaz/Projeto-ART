namespace ART.Domotica.IoTContract
{
    using System;

    using ART.Domotica.Enums;

    public class DSFamilyTempSensorSetUnitMeasurementRequestIoTContract
    {
        #region Properties

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