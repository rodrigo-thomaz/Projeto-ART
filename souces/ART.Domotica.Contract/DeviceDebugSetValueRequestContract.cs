namespace ART.Domotica.Contract
{
    using System;

    public class DeviceDebugSetValueRequestContract
    {
        #region Properties

        public Guid DeviceDatasheetId
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