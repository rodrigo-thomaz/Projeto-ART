namespace ART.Domotica.IoTContract
{
    using System;

    public class ESPDeviceUpdatePinsResponseIoTContract
    {
        #region Properties

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