namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceMQGetModel
    {
        #region Properties

        public string ClientId
        {
            get; set;
        }

        public Guid DeviceDatasheetId
        {
            get; set;
        }

        public Guid DeviceMQId
        {
            get; set;
        }

        public string Topic
        {
            get; set;
        }

        public string User
        {
            get; set;
        }

        #endregion Properties
    }
}