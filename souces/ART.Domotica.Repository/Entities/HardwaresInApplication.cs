namespace ART.Domotica.Repository.Entities
{
    using ART.Infra.CrossCutting.Repository;
    using System;

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

        #endregion Properties
    }
}