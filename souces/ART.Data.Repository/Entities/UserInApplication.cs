namespace ART.Data.Repository.Entities
{
    using System;

    public class UserInApplication
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

        public User User
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