namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceSerialSetPinModel
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

        public CommunicationDirection Direction
        {
            get; set;
        }

        public short Value
        {
            get; set;
        }

        #endregion Properties
    }
}