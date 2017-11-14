namespace ART.Domotica.Contract
{
    using System;

    public class ESPDeviceDeleteFromApplicationRequestContract
    {
        #region Properties

        public Guid DeviceInApplicationId
        {
            get; set;
        }

        #endregion Properties
    }
}