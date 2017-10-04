using System;

namespace ART.Seguranca.DistributedServices.Entities
{
    public class UsersInApplication
    {
        #region Primitive Properties

        public Guid UserId { get; set; }
        public Guid ApplicationId { get; set; }

        #endregion

        #region Navigation Properties

        public ApplicationUser User { get; set; }
        public Application Application { get; set; }

        #endregion
    }
}