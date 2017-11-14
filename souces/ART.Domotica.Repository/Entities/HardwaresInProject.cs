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

        public DeviceInApplication DeviceInApplication
        {
            get; set;
        }

        public Guid DeviceInApplicationId
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