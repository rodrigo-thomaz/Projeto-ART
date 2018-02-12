namespace ART.Domotica.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceDatasheet : IEntity<Guid>
    {
        #region Properties

        public ICollection<DeviceDatasheetBinary> DeviceDatasheetBinaries
        {
            get; set;
        }

        public ICollection<DeviceBase> DevicesBase
        {
            get; set;
        }

        public DeviceType DeviceType
        {
            get; set;
        }

        public DeviceTypeEnum DeviceTypeId
        {
            get; set;
        }

        public bool HasDeviceSensors
        {
            get; set;
        }

        public bool HasDeviceSerial
        {
            get; set;
        }

        public Guid Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        #endregion Properties
    }
}