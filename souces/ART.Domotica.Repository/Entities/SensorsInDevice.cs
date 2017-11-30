namespace ART.Domotica.Repository.Entities
{
    using System;

    public class SensorsInDevice
    {
        #region Properties

        public DeviceBase DeviceBase
        {
            get; set;
        }

        public Guid DeviceBaseId
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