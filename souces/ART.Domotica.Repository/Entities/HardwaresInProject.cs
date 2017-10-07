namespace ART.Domotica.Repository.Entities
{
    using System;

    public class HardwaresInProject
    {
        #region Properties

        public ApplicationUser CreateByApplicationUser
        {
            get; set;
        }

        public Guid CreateByApplicationUserId
        {
            get; set;
        }

        public DateTime CreateDate
        {
            get; set;
        }

        public HardwaresInApplication HardwaresInApplication
        {
            get; set;
        }

        public Guid HardwaresInApplicationId
        {
            get; set;
        }

        public Guid Id
        {
            get; set;
        }

        public Project Project
        {
            get; set;
        }

        public Guid ProjectId
        {
            get; set;
        }

        #endregion Properties
    }
}