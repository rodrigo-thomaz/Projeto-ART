namespace ART.Domotica.Contract
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceDebugSetValueRequestContract
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