using System;

namespace ART.Data.Repository.Entities
{
    public class UserInApplication
    {
        #region Primitive Properties

        public Guid UserId { get; set; }
        public Guid ApplicationId { get; set; }

        #endregion

        #region Navigation Properties

        public User User { get; set; }
        public Application Application { get; set; }

        #endregion
    }
}