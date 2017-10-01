using System;
using System.Collections.Generic;

namespace ART.MQ.Worker.Entities
{
    public abstract class HardwareBase : IEntity<Guid>
    {
        #region Primitive Properties

        public Guid Id { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<HardwareInSpace> HardwaresInSpace { get; set; }

        #endregion
    }
}