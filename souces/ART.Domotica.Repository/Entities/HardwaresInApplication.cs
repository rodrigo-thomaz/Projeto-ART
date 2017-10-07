namespace ART.Domotica.Repository.Entities
{
    using ART.Infra.CrossCutting.Repository;
    using System;
    using System.Collections.Generic;

    public class HardwaresInApplication : IEntity<Guid>
    {
        #region Properties

        public Guid Id
        {
            get; set;
        }

        public Application Application
        {
            get; set;
        }

        public Guid ApplicationId
        {
            get; set;
        }

        public HardwareBase HardwareBase
        {
            get; set;
        }

        public Guid HardwareBaseId
        {
            get; set;
        }

        public ICollection<HardwaresInProject> HardwaresInProject
        {
            get; set;
        }

        #endregion Properties
    }
}