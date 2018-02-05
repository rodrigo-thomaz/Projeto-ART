namespace ART.Domotica.Model
{
    using System;

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

        public Guid DeviceDatasheetId
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