namespace ART.Domotica.Model
{
    using System;

    public class DeviceSerialGetModel
    {
        #region Properties

        public Guid DeviceDatasheetId
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

        public Guid DeviceId
        {
            get; set;
        }

        public bool HasTX
        {
            get; set;
        }

        public bool HasRX
        {
            get; set;
        }

        #endregion Properties
    }
}