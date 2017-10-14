namespace ART.Domotica.Worker.IConsumers
{
    using System.Collections.Generic;

    using ART.Domotica.Contract;

    public interface IHardwareConsumer
    {
        #region Methods

        void UpdatePins(List<HardwareUpdatePinsContract> contracts);

        #endregion Methods
    }
}