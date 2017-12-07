namespace ART.Domotica.Model
{
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

        #endregion Properties
    }
}