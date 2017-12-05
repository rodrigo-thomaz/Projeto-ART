namespace ART.Domotica.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    using ART.Infra.CrossCutting.Repository;

    public class DeviceInApplication : IEntity
    {
        #region Properties

        public Application Application
        {
            get; set;
        }

        public Guid ApplicationId
        {
            get; set;
        }

        public ApplicationUser CreateByApplicationUser
        {
            get; set;
        }

        public Guid CreateByApplicationUserId
        {
            get; set;
        }

        public DateTime CreateDate
        {
            get; set;
        }

        public HardwareBase HardwareBase
        {
            get; set;
        }

        public Guid HardwareId
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