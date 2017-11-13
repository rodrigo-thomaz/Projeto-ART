namespace ART.Domotica.IoTContract
{
    using System;

    public class IoTRequestContract
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