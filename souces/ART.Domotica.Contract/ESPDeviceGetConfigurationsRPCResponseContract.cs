namespace ART.Domotica.Contract
{
    using System;
    using System.Collections.Generic;

    public class ESPDeviceGetConfigurationsRPCResponseContract
    {
        #region Properties

        public Guid DeviceDatasheetId
        {
            get; set;
        }

        public DeviceDebugDetailResponseContract DeviceDebug
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

        public List<DeviceSerialDetailResponseContract> DeviceSerial { get; set; }

        public DeviceWiFiDetailResponseContract DeviceWiFi
        {
            get; set;
        }

        public bool HasSensor
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