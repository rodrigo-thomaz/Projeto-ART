namespace ART.Domotica.Model
{
    using ART.Domotica.Enums;
    using System;
    using System.Collections.Generic;

    public class DeviceSensorsGetModel
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

        public List<SensorInDeviceGetModel> SensorInDevice
        {
            get; set;
        }

        public DeviceDatasheetEnum DeviceDatasheetId
        {
            get; set;
        }

        #endregion Properties
    }
}