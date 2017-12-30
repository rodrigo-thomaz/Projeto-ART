namespace ART.Domotica.Contract
{
    using System;

    using ART.Domotica.Enums;

    public class ESPDeviceGetConfigurationsRPCResponseContract
    {
        #region Properties

        public Guid? ApplicationId
        {
            get; set;
        }

        public DeviceDatasheetEnum DeviceDatasheetId
        {
            get; set;
        }

        public Guid DeviceId
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