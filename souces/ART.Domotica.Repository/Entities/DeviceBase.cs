namespace ART.Domotica.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public abstract class DeviceBase : IEntity<Guid>
    {
        #region Properties

        public DateTime CreateDate
        {
            get; set;
        }

        public DeviceBinary DeviceBinary
        {
            get; set;
        }

        public DeviceDatasheet DeviceDatasheet
        {
            get; set;
        }

        public Guid DeviceDatasheetId
        {
            get; set;
        }

        public DeviceDebug DeviceDebug
        {
            get; set;
        }

        public DeviceDisplay DeviceDisplay
        {
            get; set;
        }

        public DeviceMQ DeviceMQ
        {
            get; set;
        }

        public DeviceNTP DeviceNTP
        {
            get; set;
        }

        public DeviceSensor DeviceSensor
        {
            get; set;
        }

        public ICollection<DeviceSerial> DeviceSerial
        {
            get; set;
        }

        public ICollection<DeviceInApplication> DevicesInApplication
        {
            get; set;
        }

        public DeviceTypeEnum DeviceTypeId
        {
            get; set;
        }

        public DeviceWiFi DeviceWiFi
        {
            get; set;
        }

        public Guid Id
        {
            get; set;
        }

        public string Label
        {
            get; set;
        }

        #endregion Properties
    }
}