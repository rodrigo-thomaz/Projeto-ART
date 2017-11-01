namespace ART.Domotica.Contract
{
    using System;

    public class ESPDeviceGetInApplicationForDeviceRequestContract
    {
        #region Properties

        public string ChipId { get; set; }
        public string FlashChipId { get; set; }
        public string MacAddress { get; set; }

        #endregion Properties
    }
}