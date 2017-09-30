using System;

namespace ART.MQ.Consumer.Entities
{
    public class HardwareInSpace
    {
        #region Primitive Properties

        public Guid HardwareBaseId { get; set; }
        public Guid SpaceId { get; set; }

        #endregion

        #region Navigation Properties

        public HardwareBase HardwareBase { get; set; }
        public Space Space { get; set; }

        #endregion
    }
}