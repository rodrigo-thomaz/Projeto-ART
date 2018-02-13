namespace ART.Domotica.Model
{
    using System;
    using System.Collections.Generic;

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

        public DeviceDisplayGetModel DeviceDisplay
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

        public DeviceSensorGetModel DeviceSensor
        {
            get; set;
        }

        public List<DeviceSerialGetModel> DeviceSerial
        {
            get; set;
        }

        public DeviceTypeEnum DeviceTypeId
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