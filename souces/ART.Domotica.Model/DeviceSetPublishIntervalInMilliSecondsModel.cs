namespace ART.Domotica.Model
{
    using System;

    public class DeviceSetPublishIntervalInMilliSecondsModel
    {
        #region Properties

        public Guid DeviceDatasheetId
        {
            get; set;
        }

        public Guid DeviceId
        {
            get; set;
        }

        public long PublishIntervalInMilliSeconds
        {
            get; set;
        }

        #endregion Properties
    }
}