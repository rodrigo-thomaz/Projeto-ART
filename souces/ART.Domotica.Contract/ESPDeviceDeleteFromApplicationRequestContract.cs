namespace ART.Domotica.Contract
{
    using System;

    using ART.Domotica.Enums;

    public class ESPDeviceDeleteFromApplicationRequestContract
    {
        #region Properties

        public DeviceDatasheetEnum DeviceDatasheetId
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