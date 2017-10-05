namespace ART.MQ.Common.Contracts
{
    using System;

    public class DSFamilyTempSensorGetContract
    {
        #region Properties

        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public string Session
        {
            get; set;
        }

        #endregion Properties
    }
}