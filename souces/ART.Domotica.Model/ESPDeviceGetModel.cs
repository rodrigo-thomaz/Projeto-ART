namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class ESPDeviceGetModel
    {
        #region Properties

        public DeviceBinaryGetModel DeviceBinary
        {
            get; set;
        }

        public Guid DeviceDatasheetId
        {
            get; set;
        }

        public DeviceDebugGetModel DeviceDebug
        {
            get; set;
        }

        public Guid DeviceId
        {
            get; set;
        }

        public DeviceMQGetModel DeviceMQ
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

        public DeviceWiFiGetModel DeviceWiFi
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