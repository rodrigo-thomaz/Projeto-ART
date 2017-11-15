namespace ART.Domotica.Contract
{
    using System;

    public class DSFamilyTempSensorSetScaleRequestContract
    {
        #region Properties

        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public byte TemperatureScaleId
        {
            get; set;
        }

        #endregion Properties
    }
}