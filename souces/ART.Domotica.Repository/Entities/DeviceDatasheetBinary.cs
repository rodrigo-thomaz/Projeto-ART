namespace ART.Domotica.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    using ART.Infra.CrossCutting.Repository;

    public class DeviceDatasheetBinary : IEntity
    {
        #region Properties

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

        public DeviceDatasheetBinaryBuffer DeviceDatasheetBinaryBuffer
        {
            get; set;
        }

        public Guid DeviceDatasheetId
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