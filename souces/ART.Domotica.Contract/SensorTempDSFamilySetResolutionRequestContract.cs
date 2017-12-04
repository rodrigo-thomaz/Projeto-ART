namespace ART.Domotica.Contract
{
    using System;

    public class SensorTempDSFamilySetResolutionRequestContract
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