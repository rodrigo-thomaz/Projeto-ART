namespace ART.Domotica.IoTContract
{
    using ART.Domotica.Enums;
    using System;

    public class ESPDeviceUpdatePinsResponseIoTContract
    {
        #region Properties

        public Guid DeviceId
        {
            get; set;
        }

        public DeviceDatasheetEnum DeviceDatasheetId
        {
            get; set;
        }

        public int FlashChipId
        {
            get; set;
        }

        public double NextFireTimeInSeconds
        {
            get; set;
        }

        public string Pin
        {
            get; set;
        }

        #endregion Properties
    }
}