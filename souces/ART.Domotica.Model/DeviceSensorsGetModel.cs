namespace ART.Domotica.Model
{
    using System;
    using System.Collections.Generic;

    public class DeviceSensorsGetModel
    {
        #region Properties

        public Guid DeviceDatasheetId
        {
            get; set;
        }

        public Guid DeviceSensorsId
        {
            get; set;
        }

        public int PublishIntervalInMilliSeconds
        {
            get; set;
        }

        public int ReadIntervalInMilliSeconds
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