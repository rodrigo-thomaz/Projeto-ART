namespace ART.Domotica.Model
{
    using System;

    public class ESPDeviceGetModel
    {
        #region Properties

        public Guid ApplicationId
        {
            get; set;
        }

        public int ChipId
        {
            get; set;
        }

        public long CreateDate
        {
            get; set;
        }

        public Guid DeviceId
        {
            get; set;
        }

        public DeviceNTPGetModel DeviceNTP
        {
            get; set;
        }

        public int FlashChipId
        {
            get; set;
        }

        public string Label
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