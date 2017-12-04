namespace ART.Domotica.IoTContract
{
    using System;

    public class SensorTempDSFamilySetResolutionRequestIoTContract
    {
        #region Properties

        public Guid SensorTempDSFamilyId
        {
            get; set;
        }

        public byte SensorTempDSFamilyResolutionId
        {
            get; set;
        }

        #endregion Properties
    }
}