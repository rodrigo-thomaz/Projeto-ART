using System;

namespace ART.Domotica.DistributedServices.Entities
{
    public class SensorInSpace
    {
        #region Primitive Properties

        public Guid SensorId { get; set; }
        public Guid SpaceId { get; set; }

        #endregion

        #region Navigation Properties

        public SensorBase SensorBase { get; set; }
        public Space Space { get; set; }

        #endregion
    }
}