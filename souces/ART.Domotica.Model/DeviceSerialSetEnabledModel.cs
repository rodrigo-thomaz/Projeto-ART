namespace ART.Domotica.Model
{
    using System;

    public class DeviceSerialSetEnabledModel
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

        public Guid DeviceSerialId
        {
            get; set;
        }

        public bool Enabled
        {
            get; set;
        }

        #endregion Properties
    }
}