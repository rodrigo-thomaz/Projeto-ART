namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceSerialGetModel
    {
        #region Properties

        public bool? AllowPinSwapRX
        {
            get; set;
        }

        public bool? AllowPinSwapTX
        {
            get; set;
        }

        public Guid DeviceDatasheetId
        {
            get; set;
        }

        public Guid DeviceId
        {
            get; set;
        }

        public Guid DeviceSerialId
        {
            get; set;
        }

        public DeviceTypeEnum DeviceTypeId
        {
            get; set;
        }

        public bool Enabled
        {
            get; set;
        }

        public short Index
        {
            get; set;
        }

        public short? PinRX
        {
            get; set;
        }

        public short? PinTX
        {
            get; set;
        }

        public SerialModeEnum SerialMode
        {
            get; set;
        }

        #endregion Properties
    }
}