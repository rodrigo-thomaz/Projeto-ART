using ART.Domotica.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ART.Domotica.Worker.IConsumers
{
    public interface IHardwareConsumer
    {
        Task UpdatePinsAsync(List<HardwareUpdatePinsContract> contracts);
    }
}