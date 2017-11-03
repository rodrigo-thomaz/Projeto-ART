namespace ART.Domotica.Model
{
    using System;

    public class ESPDeviceGetListModel
    {
        #region Properties

        public long CreateDate
        {
            get; set;
        }

        public Guid HardwareInApplicationId
        {
            get; set;
        }

        public Guid HardwareId
        {
            get; set;
        }

        public int ChipId
        {
            get; set;
        }

        public int FlashChipId
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