namespace ART.Data.Repository.Entities
{
    using System;

    public class UsersInApplication
    {
        #region Properties

        public Application Application
        {
            get; set;
        }

        public Guid ApplicationId
        {
            get; set;
        }

        public ApplicationUser ApplicationUser
        {
            get; set;
        }

        public Guid UserId
        {
            get; set;
        }

        #endregion Properties
    }
}