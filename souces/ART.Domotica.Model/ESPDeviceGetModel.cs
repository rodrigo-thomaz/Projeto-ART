namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class ESPDeviceGetModel
    {
        #region Properties

        public DeviceDatasheetEnum DeviceDatasheetId
        {
            get; set;
        }

        public Guid DeviceId
        {
            get; set;
        }

        public DeviceNTPGetModel DeviceNTP
        {
            get; set;
        }

        public DeviceWiFiGetModel DeviceWiFi
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

        #endregion Properties
    }
}