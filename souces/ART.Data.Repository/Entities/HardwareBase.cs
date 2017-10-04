namespace ART.Data.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    public abstract class HardwareBase : IEntity<Guid>
    {
        #region Properties

        public ICollection<HardwareInApplication> HardwaresInApplication
        {
            get; set;
        }

        public Guid Id
        {
            get; set;
        }

        #endregion Properties
    }
}