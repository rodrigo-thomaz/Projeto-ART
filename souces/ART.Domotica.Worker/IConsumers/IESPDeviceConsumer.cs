namespace ART.Domotica.Worker.IConsumers
{
    using System.Collections.Generic;

    using ART.Domotica.Contract;

    public interface IESPDeviceConsumer
    {
        #region Methods

        void UpdatePins(List<ESPDeviceUpdatePinsContract> contracts, double nextFireTimeInSeconds);

        #endregion Methods
    }
}