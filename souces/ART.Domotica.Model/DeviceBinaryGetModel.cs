namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceBinaryGetModel
    {
        #region Properties

        public DateTime CreateDate
        {
            get; set;
        }

        public Guid DeviceBinaryId
        {
            get; set;
        }

        public DeviceDatasheetEnum DeviceDatasheetId
        {
            get; set;
        }

        public DateTime UpdateDate
        {
            get; set;
        }

        public string Version
        {
            get; set;
        }

        #endregion Properties
    }
}