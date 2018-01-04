namespace ART.Domotica.Contract
{
    using System;

    using ART.Domotica.Enums;

    public class ESPDeviceGetConfigurationsRPCResponseContract
    {
        #region Properties

        public DeviceDatasheetEnum DeviceDatasheetId
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

        public DeviceSensorsDetailResponseContract DeviceSensors
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