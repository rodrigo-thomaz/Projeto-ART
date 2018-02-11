namespace ART.Domotica.IoTContract
{
    using System;

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

        public bool HasRX
        {
            get; set;
        }

        public bool HasTX
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

        #endregion Properties
    }
}