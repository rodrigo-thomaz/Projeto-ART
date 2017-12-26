namespace ART.Domotica.IoTContract
{
    using System;

    using ART.Domotica.Enums;

    public class ESPDeviceUpdatePinsResponseIoTContract
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