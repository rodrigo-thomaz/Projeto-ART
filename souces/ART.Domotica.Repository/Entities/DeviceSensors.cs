namespace ART.Domotica.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    using ART.Infra.CrossCutting.Repository;

    public class DeviceSensors : IEntity
    {
        #region Properties

        public DeviceBase DeviceBase
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

        public long PublishIntervalInMilliSeconds
        {
            get; set;
        }

        public long ReadIntervalInMilliSeconds
        {
            get; set;
        }

        public ICollection<SensorInDevice> SensorInDevice
        {
            get; set;
        }

        #endregion Properties
    }
}