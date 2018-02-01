namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class ESPDeviceGetByPinModel
    {
        #region Properties

        public Guid DeviceDatasheetId
        {
            get; set;
        }

        public Guid DeviceId
        {
            get; set;
        }

        #endregion Properties
    }
}