namespace ART.Domotica.Contract
{
    using System;

    public class DSFamilyTempSensorSetResolutionRequestContract
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