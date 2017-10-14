using System;

namespace ART.Domotica.Contract
{
    public class HardwareUpdatePinsContract
    {
        public Guid HardwareId { get; set; }
        public string Pin { get; set; }
    }
}