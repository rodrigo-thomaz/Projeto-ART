using ART.Infra.CrossCutting.Repository;
using System;
using System.Collections.Generic;

namespace ART.Domotica.Repository.Entities
{
    public abstract class DeviceBase : IEntity<Guid>
    {
        #region Properties

        public DeviceMQ DeviceMQ
        {
            get; set;
        }

        public DeviceNTP DeviceNTP
        {
            get; set;
        }

        public DeviceSensors DeviceSensors
        {
            get; set;
        }













        public DateTime CreateDate
        {
            get; set;
        }

        public ICollection<HardwareInApplication> DevicesInApplication
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