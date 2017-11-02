using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ART.Domotica.Contract
{
    public class ESPDeviceGetConfigurationsContractResponse
    {
        public string BrokerHost { get; set; }
        public int BrokerPort { get; set; }
    }
}
