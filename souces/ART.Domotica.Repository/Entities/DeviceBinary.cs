namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceBinary : IEntity
    {
        #region Properties

        public DeviceBase DeviceBase
        {
            get; set;
        }

        public DeviceDatasheetBinary DeviceDatasheetBinary
        {
            get; set;
        }

        public Guid DeviceDatasheetBinaryId
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

        public DateTime UpdateDate
        {
            get; set;
        }

        #endregion Properties
    }
}