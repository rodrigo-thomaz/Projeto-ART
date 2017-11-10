namespace ART.Domotica.Worker.IConsumers
{
    using System;

    public interface IESPDeviceConsumer
    {
        #region Methods

        void UpdatePins(DateTimeOffset nextFireTimeUtc);

        #endregion Methods
    }
}