namespace ART.Domotica.Model
{
    using System;

    public class ESPDeviceAdminGetModel
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

        public Guid DeviceDatasheetId
        {
            get; set;
        }

        public Guid DeviceId
        {
            get; set;
        }

        public int FlashChipId
        {
            get; set;
        }

        public bool InApplication
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