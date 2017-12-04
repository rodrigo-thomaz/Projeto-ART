namespace ART.Domotica.Repository.Entities
{
    using System;

    public class SensorsInDevice
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

        public Sensor Sensor
        {
            get; set;
        }

        public Guid SensorId
        {
            get; set;
        }

        public short Ordination
        {
            get; set;
        }

        #endregion Properties
    }
}