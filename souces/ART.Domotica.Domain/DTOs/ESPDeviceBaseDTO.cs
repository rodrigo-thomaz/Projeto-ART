using System;

namespace ART.Domotica.Domain.DTOs
{
    public class ESPDeviceBaseDTO
    {
        public Guid ESPDeviceId { get; set; }

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

        public string Pin
        {
            get; set;
        }

        public int TimeOffset
        {
            get; set;
        }

        public DateTime CreateDate { get; set; }

        public Guid? HardwaresInApplicationId { get; set; }
    }
}
