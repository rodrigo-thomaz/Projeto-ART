namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Infra.CrossCutting.Repository;

    public class DeviceSerial : IEntity<Guid>
    {
        #region Properties

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

        public bool HasTX
        {
            get; set;
        }

        public bool HasRX
        {
            get; set;
        }

        public int? PinTX
        {
            get; set;
        }

        public int? PinRX
        {
            get; set;
        }

        public bool? AllowPinSwapRX
        {
            get; set;
        }

        public bool? AllowPinSwapTX
        {
            get; set;
        }

        #endregion Properties
    }
}