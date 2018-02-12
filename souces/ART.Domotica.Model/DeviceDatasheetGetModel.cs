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

        public DeviceTypeEnum DeviceTypeId
        {
            get; set;
        }

        public bool HasDeviceSensors
        {
            get; set;
        }

        public bool HasDeviceSerial
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