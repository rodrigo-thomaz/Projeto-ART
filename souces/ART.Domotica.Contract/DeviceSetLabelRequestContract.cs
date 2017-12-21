namespace ART.Domotica.Contract
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceSetLabelRequestContract
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

        public string Label
        {
            get; set;
        }

        #endregion Properties
    }
}