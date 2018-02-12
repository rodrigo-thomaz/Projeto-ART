namespace ART.Domotica.Model
{
    using System;
    using System.Collections.Generic;

    using ART.Domotica.Enums;

    public class DeviceSensorGetModel
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

        public long ReadIntervalInMilliSeconds
        {
            get; set;
        }

        public List<SensorInDeviceGetModel> SensorInDevice
        {
            get; set;
        }

        #endregion Properties
    }
}