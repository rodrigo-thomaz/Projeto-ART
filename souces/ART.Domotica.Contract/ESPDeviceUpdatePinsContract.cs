namespace ART.Domotica.Contract
{
    using System;

    public class ESPDeviceUpdatePinsContract
    {
        #region Properties

        public int FlashChipId
        {
            get; set;
        }

        public Guid HardwareId
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