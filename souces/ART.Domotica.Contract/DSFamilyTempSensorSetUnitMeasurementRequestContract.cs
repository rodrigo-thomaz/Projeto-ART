namespace ART.Domotica.Contract
{
    using System;

    using ART.Domotica.Enums.SI;

    public class DSFamilyTempSensorSetUnitMeasurementRequestContract
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