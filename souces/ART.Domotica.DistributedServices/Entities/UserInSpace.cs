using System;

namespace ART.Domotica.DistributedServices.Entities
{
    public class UserInSpace
    {
        #region Primitive Properties

        public Guid UserId { get; set; }
        public Guid SpaceId { get; set; }

        #endregion

        #region Navigation Properties

        public User User { get; set; }
        public Space Space { get; set; }

        #endregion
    }
}