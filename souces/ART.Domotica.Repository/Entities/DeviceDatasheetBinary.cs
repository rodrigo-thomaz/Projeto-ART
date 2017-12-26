namespace ART.Domotica.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

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

        public ICollection<DeviceBinary> DeviceBinaries
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

        #endregion Properties
    }
}