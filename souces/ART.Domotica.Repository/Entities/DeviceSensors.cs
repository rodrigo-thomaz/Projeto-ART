namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Infra.CrossCutting.Repository;

    public class DeviceSensors : IEntity<Guid>
    {
        #region Properties
               
        public Guid Id
        {
            get; set;
        }
        public int PublishIntervalInSeconds { get; set; }

        public DeviceBase DeviceBase { get; set; }

        #endregion Properties
    }
}