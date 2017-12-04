namespace ART.Domotica.Model
{
    using System;

    public class SensorTempDSFamilySetResolutionCompletedModel
    {
        #region Properties

        public Guid DeviceId
        {
            get; set;
        }

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