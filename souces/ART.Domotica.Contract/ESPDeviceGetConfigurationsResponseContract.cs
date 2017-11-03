using System;

namespace ART.Domotica.Contract
{
    public class ESPDeviceGetConfigurationsResponseContract
    {
        public string BrokerHost { get; set; }
        public int BrokerPort { get; set; }
        public string BrokerUser { get; set; }
        public string BrokerPassword { get; set; }
        public Guid HardwareId
        {
            get; set;
        }
        public Guid? HardwareInApplicationId
        {
            get; set;
        }
    }
}
