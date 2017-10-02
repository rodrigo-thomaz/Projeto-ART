using System;
using System.Collections.Generic;

namespace ART.Data.Repository.Entities
{
    public class Space : IEntity<Guid>
    {
        #region Primitive Properties

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<UserInSpace> UsersInSpace { get; set; }
        public ICollection<HardwareInSpace> HardwaresInSpace { get; set; }

        #endregion
    }
}