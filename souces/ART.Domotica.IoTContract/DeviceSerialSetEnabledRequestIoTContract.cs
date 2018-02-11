namespace ART.Domotica.IoTContract
{
    using System;

    public class DeviceSerialSetEnabledRequestIoTContract
    {
        #region Properties

        public Guid DeviceSerialId
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