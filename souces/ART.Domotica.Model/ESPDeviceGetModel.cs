namespace ART.Domotica.Model
{
    using ART.Domotica.Enums;
    using System;

    public class ESPDeviceGetModel
    {
        #region Properties

        public Guid DeviceId
        {
            get; set;
        }

        public DeviceNTPGetModel DeviceNTP
        {
            get; set;
        }

        public DeviceSensorsGetModel DeviceSensors
        {
            get; set;
        }

        public string Label
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