namespace ART.Domotica.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    using ART.Infra.CrossCutting.Repository;

    public abstract class HardwareBase : IEntity<Guid>
    {
        #region Properties

        public DateTime CreateDate
        {
            get; set;
        }

        public ICollection<DeviceInApplication> DevicesInApplication
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