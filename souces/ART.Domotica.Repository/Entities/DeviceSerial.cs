namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceSerial : IEntity<Guid>
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

        public int BaudRate
        {
            get; set;
        }

        public DeviceBase DeviceBase
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

        public bool Enabled
        {
            get; set;
        }

        public Guid Id
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