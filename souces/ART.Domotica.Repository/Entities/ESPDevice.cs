﻿namespace ART.Domotica.Repository.Entities
{
    public class ESPDevice : DeviceBase
    {
        #region Properties

        public int ChipId
        {
            get; set;
        }

        public long ChipSize
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

        public string SDKVersion
        {
            get; set;
        }

        #endregion Properties
    }
}