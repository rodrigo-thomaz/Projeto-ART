namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;
    using System.Collections.Generic;

    public class DeviceDatasheetBinary : IEntity
    {
        #region Properties

        public byte[] Binary
        {
            get; set;
        }

        public DateTime CreateDate
        {
            get; set;
        }

        public DeviceDatasheet DeviceDatasheet
        {
            get; set;
        }

        public DeviceDatasheetEnum DeviceDatasheetId
        {
            get; set;
        }

        public Guid Id
        {
            get; set;
        }

        public string Version
        {
            get; set;
        }

        public ICollection<DeviceBinary> DeviceBinaries
        {
            get; set;
        }

        #endregion Properties
    }
}