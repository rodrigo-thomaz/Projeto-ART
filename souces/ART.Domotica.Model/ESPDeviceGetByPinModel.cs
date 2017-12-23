namespace ART.Domotica.Model
{
    using ART.Domotica.Enums;
    using System;

    public class ESPDeviceGetByPinModel
    {
        #region Properties

        public Guid DeviceId
        {
            get; set;
        }

        public DeviceDatasheetEnum DeviceDatasheetId
        {
            get; set;
        }

        #endregion Properties
    }
}