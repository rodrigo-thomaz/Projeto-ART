using ART.Domotica.Contract;
using System.Collections.Generic;

namespace ART.Domotica.Worker.IConsumers
{
    public interface IThermometerDeviceConsumer
    {
        #region Methods

        void UpdatePins(List<ThermometerDeviceUpdatePinsContract> contracts);

        #endregion Methods
    }
}