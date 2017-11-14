namespace ART.Domotica.IoTContract
{
    using System;

    public class DSFamilyTempSensorSetResolutionRequestIoTContract
    {
        #region Properties

        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public byte DSFamilyTempSensorResolutionId
        {
            get; set;
        }

        #endregion Properties
    }
}