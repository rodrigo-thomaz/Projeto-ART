namespace ART.Domotica.Contract
{
    using System;

    public class ThermometerDeviceUpdatePinsContract
    {
        #region Properties

        public Guid HardwareId
        {
            get; set;
        }

        public string Pin
        {
            get; set;
        }

        #endregion Properties
    }
}