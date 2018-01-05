namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceDebugSetValueModel
    {
        #region Properties

        public DeviceDatasheetEnum DeviceDatasheetId
        {
            get; set;
        }

        public Guid DeviceDebugId
        {
            get; set;
        }

        public bool Value
        {
            get; set;
        }

        #endregion Properties
    }
}