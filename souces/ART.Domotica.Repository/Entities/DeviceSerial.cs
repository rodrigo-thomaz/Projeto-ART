namespace ART.Domotica.Repository.Entities
{
    using System;
    using System.Collections.Generic;

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

        #endregion Properties
    }
}