namespace ART.Data.Repository.Entities
{
    using ART.Infra.CrossCutting.Repository;
    using System;
    using System.Collections.Generic;

    public class User : IEntity<Guid>
    {
        #region Properties

        public Guid Id
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