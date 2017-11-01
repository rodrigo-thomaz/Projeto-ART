namespace ART.Domotica.Contract
{
    using System;

    public class ESPDeviceGetInApplicationForDeviceResponseContract
    {
        #region Properties

        public Guid HardwareId
        {
            get; set;
        }

        public Guid HardwaresInApplicationId
        {
            get; set;
        }

        #endregion Properties
    }
}