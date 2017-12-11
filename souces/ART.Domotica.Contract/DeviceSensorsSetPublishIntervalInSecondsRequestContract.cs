namespace ART.Domotica.Contract
{
    using System;

    public class DeviceSensorsSetPublishIntervalInSecondsRequestContract
    {
        #region Properties

        public Guid DeviceSensorsId
        {
            get; set;
        }

        public int PublishIntervalInSeconds
        {
            get; set;
        }

        #endregion Properties
    }
}