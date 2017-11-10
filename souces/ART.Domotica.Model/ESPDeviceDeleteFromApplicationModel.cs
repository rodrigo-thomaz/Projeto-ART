namespace ART.Domotica.Model
{
    using System;

    public class ESPDeviceDeleteFromApplicationModel
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