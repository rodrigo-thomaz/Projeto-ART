namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

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

        public DeviceTypeEnum DeviceTypeId
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