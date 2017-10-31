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

        public Guid HardwaresInApplicationId
        {
            get; set;
        }

        public Guid HardwareId
        {
            get; set;
        }

        public string ChipId
        {
            get; set;
        }

        public string FlashChipId
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