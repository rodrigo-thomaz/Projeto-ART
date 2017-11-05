namespace ART.Domotica.Contract
{
    using System;

    public class ESPDeviceDeleteFromApplicationResponseContract
    {
        #region Properties

        public Guid HardwareInApplicationId
        {
            get; set;
        }

        public Guid HardwareId
        {
            get; set;
        }

        #endregion Properties
    }
}