using ART.Domotica.Enums;
using ART.Infra.CrossCutting.Repository;
using System;

namespace ART.Domotica.Repository.Entities
{
    public class DeviceBinary : IEntity
    {
        public DeviceBase DeviceBase
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
    }
}
