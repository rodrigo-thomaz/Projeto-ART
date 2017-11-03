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

        public Guid HardwareInApplicationId
        {
            get; set;
        }

        #endregion Properties
    }
}