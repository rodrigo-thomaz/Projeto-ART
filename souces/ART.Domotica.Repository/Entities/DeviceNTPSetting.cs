namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Infra.CrossCutting.Repository;

    public class DeviceNTPSetting : IEntity<Guid>
    {
        #region Properties

        public int TimeOffset
        {
            get; set;
        }

        public DeviceBase DeviceBase
        {
            get; set;
        }

        public Guid Id
        {
            get; set;
        }

        public int UpdateInterval
        {
            get; set;
        }

        #endregion Properties
    }
}