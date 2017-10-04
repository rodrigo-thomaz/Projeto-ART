namespace ART.Data.Repository.Entities
{
    using ART.Infra.CrossCutting.Repository;
    using System;
    using System.Collections.Generic;

    public class Application : IEntity<Guid>
    {
        #region Properties

        public string Description
        {
            get; set;
        }

        public ICollection<HardwareInApplication> HardwaresInApplication
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

        public ICollection<UserInApplication> UsersInApplication
        {
            get; set;
        }

        #endregion Properties
    }
}