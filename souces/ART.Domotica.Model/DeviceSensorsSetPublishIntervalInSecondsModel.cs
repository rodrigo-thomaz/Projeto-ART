namespace ART.Domotica.Model
{
    using System;

    public class DeviceSensorsSetPublishIntervalInSecondsModel
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