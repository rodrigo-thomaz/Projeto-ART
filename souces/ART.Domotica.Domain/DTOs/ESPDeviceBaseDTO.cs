namespace ART.Domotica.Domain.DTOs
{
    using System;

    public class ESPDeviceBaseDTO
    {
        #region Properties

        public int ChipId
        {
            get; set;
        }

        public DateTime CreateDate
        {
            get; set;
        }

        public Guid ESPDeviceId
        {
            get; set;
        }

        public int FlashChipId
        {
            get; set;
        }

        public Guid? HardwaresInApplicationId
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

        public int TimeOffset
        {
            get; set;
        }

        #endregion Properties
    }
}