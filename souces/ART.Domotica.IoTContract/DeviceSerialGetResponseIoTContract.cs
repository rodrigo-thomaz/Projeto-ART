namespace ART.Domotica.IoTContract
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceSerialGetResponseIoTContract
    {
        #region Properties

        public bool AllowPinSwapRX
        {
            get; set;
        }

        public bool AllowPinSwapTX
        {
            get; set;
        }

        public Guid DeviceSerialId
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

        public short PinRX
        {
            get; set;
        }

        public short PinTX
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