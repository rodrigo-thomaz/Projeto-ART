namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceDatasheetBinaryBuffer : IEntity
    {
        #region Properties

        public byte[] Buffer
        {
            get; set;
        }

        public DeviceDatasheetBinary DeviceDatasheetBinary
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

        #endregion Properties
    }
}