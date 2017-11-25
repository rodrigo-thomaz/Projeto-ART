namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Infra.CrossCutting.Repository;

    public class DeviceNTP : IEntity<Guid>
    {
        #region Properties

        public DeviceBase DeviceBase
        {
            get; set;
        }

        public Guid Id
        {
            get; set;
        }

        public int UpdateIntervalInMilliSecond
        {
            get; set;
        }

        public byte TimeZoneId { get; set; }

        public TimeZone TimeZone { get; set; }

        #endregion Properties
    }
}