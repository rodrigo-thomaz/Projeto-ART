namespace ART.Domotica.Contract
{
    using System;

    public class ESPDeviceUpdatePinsContract
    {
        #region Properties

        public Guid HardwareId
        {
            get; set;
        }

        public int FlashChipId
        {
            get; set;
        }

        public string Pin
        {
            get; set;
        }

        public double NextFireTimeInSeconds { get; set; }

        #endregion Properties
    }
}