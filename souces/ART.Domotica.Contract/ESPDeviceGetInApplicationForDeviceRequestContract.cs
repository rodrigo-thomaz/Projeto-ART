namespace ART.Domotica.Contract
{
    using System;

    public class ESPDeviceGetInApplicationForDeviceRequestContract
    {
        #region Properties

        public int ChipId { get; set; }
        public int FlashChipId { get; set; }
        public string MacAddress { get; set; }

        #endregion Properties
    }
}