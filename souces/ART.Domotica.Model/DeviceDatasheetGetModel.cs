namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceDatasheetGetModel
    {
        #region Properties

        public Guid DeviceDatasheetId
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        #endregion Properties
    }
}