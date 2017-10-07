namespace ART.Domotica.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    using ART.Infra.CrossCutting.Repository;

    public class Application : IEntity<Guid>
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

        public ICollection<Project> Projects
        {
            get; set;
        }

        public ICollection<ApplicationUser> ApplicationUsers
        {
            get; set;
        }

        public DateTime CreateDate { get; set; }

        #endregion Properties
    }
}