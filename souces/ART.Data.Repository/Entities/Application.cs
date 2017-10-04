using System;
using System.Collections.Generic;

namespace ART.Data.Repository.Entities
{
    public class Application : IEntity<Guid>
    {
        #region Primitive Properties

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<UserInApplication> UsersInApplication { get; set; }
        public ICollection<HardwareInApplication> HardwaresInApplication { get; set; }

        #endregion
    }
}