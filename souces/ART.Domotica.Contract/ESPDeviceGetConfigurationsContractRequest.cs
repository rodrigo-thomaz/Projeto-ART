using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ART.Domotica.Contract
{
    public class ESPDeviceGetConfigurationsContractRequest
    {
        public int ChipId { get; set; }
        public int FlashChipId { get; set; }
        public string MacAddress { get; set; }
    }
}
