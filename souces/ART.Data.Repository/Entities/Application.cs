namespace ART.Data.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    using ART.Infra.CrossCutting.Repository;

    public class Application : IEntity<Guid>
    {
        #region Properties

        public string Description
        {
            get; set;
        }

        public ICollection<HardwaresInApplication> HardwaresInApplication
        {
            get; set;
        }

        public Guid Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public ICollection<UsersInApplication> UsersInApplication
        {
            get; set;
        }

        #endregion Properties
    }
}