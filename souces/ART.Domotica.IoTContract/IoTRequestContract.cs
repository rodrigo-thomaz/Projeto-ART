namespace ART.Domotica.IoTContract
{
    using System;

    public class IoTRequestContract
    {
        #region Properties

        public Guid DeviceId
        {
            get; set;
        }

        public Guid DeviceInApplicationId
        {
            get; set;
        }

        #endregion Properties
    }
}