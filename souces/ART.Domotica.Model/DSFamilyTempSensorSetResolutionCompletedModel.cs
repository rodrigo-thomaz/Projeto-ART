namespace ART.Domotica.Model
{
    using System;

    public class DSFamilyTempSensorSetResolutionCompletedModel
    {
        #region Properties

        public Guid DeviceId
        {
            get; set;
        }

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