namespace ART.MQ.DistributedServices.Models
{
    using System;

    public class DSFamilyTempSensorSetResolutionModel
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