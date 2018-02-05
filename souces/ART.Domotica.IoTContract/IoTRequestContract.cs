namespace ART.Domotica.IoTContract
{
    using System;

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