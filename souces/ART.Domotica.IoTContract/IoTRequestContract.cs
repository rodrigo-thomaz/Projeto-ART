namespace ART.Domotica.IoTContract
{
    using System;

    using ART.Domotica.Enums;

    public class IoTRequestContract
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