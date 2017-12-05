namespace ART.Domotica.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    using ART.Infra.CrossCutting.Repository;

    public class Application : IEntity<Guid>
    {
        #region Properties

        public ApplicationMQ ApplicationMQ
        {
            get; set;
        }

        public ICollection<ApplicationUser> ApplicationUsers
        {
            get; set;
        }

        public DateTime CreateDate
        {
            get; set;
        }

        public ICollection<HardwareInApplication> DevicesInApplication
        {
            get; set;
        }

        public Guid Id
        {
            get; set;
        }

        public ICollection<Project> Projects
        {
            get; set;
        }

        #endregion Properties
    }
}