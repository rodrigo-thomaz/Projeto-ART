namespace ART.Domotica.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    using ART.Infra.CrossCutting.Repository;

    public abstract class HardwareBase : IEntity<Guid>
    {
        #region Properties

        public ICollection<HardwaresInApplication> HardwaresInApplication
        {
            get; set;
        }

        public Guid Id
        {
            get; set;
        }

        public DateTime CreateDate { get; set; }

        #endregion Properties
    }
}