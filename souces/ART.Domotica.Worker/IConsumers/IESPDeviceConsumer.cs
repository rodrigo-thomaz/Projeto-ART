using ART.Domotica.Contract;
using System.Collections.Generic;

namespace ART.Domotica.Worker.IConsumers
{
    public interface IESPDeviceConsumer
    {
        void UpdatePins(List<ESPDeviceUpdatePinsContract> contracts);
    }
}