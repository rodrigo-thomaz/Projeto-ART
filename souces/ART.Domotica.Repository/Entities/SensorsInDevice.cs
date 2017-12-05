namespace ART.Domotica.Repository.Entities
{
    using ART.Infra.CrossCutting.Repository;
    using System;

    public class SensorsInDevice : IEntity
    {
        #region Properties

        public DeviceSensors DeviceSensors
        {
            get; set;
        }

        public Guid DeviceSensorsId
        {
            get; set;
        }

        public short Ordination
        {
            get; set;
        }

        public Sensor Sensor
        {
            get; set;
        }

        public Guid SensorId
        {
            get; set;
        }

        #endregion Properties
    }
}