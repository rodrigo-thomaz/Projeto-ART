namespace ART.Domotica.Contract
{
    using System;

    using ART.Domotica.Enums;

    public class ESPDeviceGetConfigurationsRPCResponseContract
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

        public DeviceInApplicationDetailResponseContract DeviceInApplication
        {
            get; set;
        }

        public DeviceMQDetailResponseContract DeviceMQ
        {
            get; set;
        }

        public DeviceNTPDetailResponseContract DeviceNTP
        {
            get; set;
        }

        public DeviceTypeEnum DeviceTypeId
        {
            get; set;
        }

        public DeviceWiFiDetailResponseContract DeviceWiFi
        {
            get; set;
        }

        public bool HasDeviceBinary
        {
            get; set;
        }

        public bool HasDeviceDebug
        {
            get; set;
        }

        public bool HasDeviceDisplay
        {
            get; set;
        }

        public bool HasDeviceMQ
        {
            get; set;
        }

        public bool HasDeviceNTP
        {
            get; set;
        }

        public bool HasDeviceSensor
        {
            get; set;
        }

        public bool HasDeviceSerial
        {
            get; set;
        }

        public bool HasDeviceWiFi
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