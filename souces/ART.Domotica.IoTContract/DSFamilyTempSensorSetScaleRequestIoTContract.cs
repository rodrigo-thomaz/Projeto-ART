namespace ART.Domotica.IoTContract
{
    using System;

    public class DSFamilyTempSensorSetScaleRequestIoTContract
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