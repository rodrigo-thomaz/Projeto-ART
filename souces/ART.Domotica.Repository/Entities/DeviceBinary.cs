using ART.Domotica.Enums;
using ART.Infra.CrossCutting.Repository;
using System;

namespace ART.Domotica.Repository.Entities
{
    public class DeviceBinary : IEntity<Guid>
    {
        #region Properties

        public Guid Id
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

        public byte[] Binary { get; set; }

        #endregion Properties
    }
}
