namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceInApplicationRemoveModel
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

        public DeviceTypeEnum DeviceTypeId
        {
            get; set;
        }

        #endregion Properties
    }
}