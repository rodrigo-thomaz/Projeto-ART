namespace ART.Domotica.Model
{
    using System;

    public class ESPDeviceDetailModel
    {
        #region Properties

        public int ChipId
        {
            get; set;
        }

        public long CreateDate
        {
            get; set;
        }

        public int FlashChipId
        {
            get; set;
        }

        public Guid HardwareId
        {
            get; set;
        }

        public Guid HardwareInApplicationId
        {
            get; set;
        }

        public string MacAddress
        {
            get; set;
        }

        #endregion Properties
    }
}