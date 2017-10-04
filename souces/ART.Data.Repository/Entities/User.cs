using System;
using System.Collections.Generic;

namespace ART.Data.Repository.Entities
{
    public class User : IEntity<Guid>
    {
        #region Primitive Properties

        public Guid Id { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<UserInApplication> UsersInApplication { get; set; }

        #endregion
    }
}