using System;

namespace ART.Data.Repository.Entities
{
    public class SensorsInDevice
    {
        #region Primitive Properties

        public Guid SensorBaseId { get; set; }
        public Guid DeviceBaseId { get; set; }

        #endregion

        #region Navigation Properties

        public SensorBase SensorBase { get; set; }
        public DeviceBase DeviceBase { get; set; }

        #endregion
    }
}
