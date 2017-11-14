namespace ART.Domotica.Model
{
    using System;

    public class ESPDeviceDeleteFromApplicationModel
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