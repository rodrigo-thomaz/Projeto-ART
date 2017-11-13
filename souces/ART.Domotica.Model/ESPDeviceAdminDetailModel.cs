namespace ART.Domotica.Model
{
    using System;

    public class ESPDeviceAdminDetailModel
    {
        #region Properties

        public bool InApplication { get; set; }

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

        public string MacAddress
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