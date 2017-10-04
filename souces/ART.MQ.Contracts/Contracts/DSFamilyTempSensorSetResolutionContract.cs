namespace ART.MQ.Common.Contracts
{
    using System;

    [Serializable]
    public class DSFamilyTempSensorSetResolutionContract
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