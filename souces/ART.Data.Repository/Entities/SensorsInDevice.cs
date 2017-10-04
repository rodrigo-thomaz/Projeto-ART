namespace ART.Data.Repository.Entities
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

        public SensorBase SensorBase
        {
            get; set;
        }

        public Guid SensorBaseId
        {
            get; set;
        }

        #endregion Properties
    }
}