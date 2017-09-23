using System;
using System.Collections.Generic;

namespace ART.Domotica.DistributedServices.Entities
{
    public class User : IEntity
    {
        #region Primitive Properties

        public Guid Id { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<UserInSpace> UsersInSpace { get; set; }

        #endregion
    }
}