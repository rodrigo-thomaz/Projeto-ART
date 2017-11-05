using System;

namespace ART.Domotica.Contract
{
    public class ESPDeviceInsertInApplicationResponseContract
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