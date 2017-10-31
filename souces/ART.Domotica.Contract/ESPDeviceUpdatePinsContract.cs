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

        public String FlashChipId
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