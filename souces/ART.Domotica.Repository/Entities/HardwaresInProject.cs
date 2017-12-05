namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Infra.CrossCutting.Repository;

    public class HardwaresInProject : IEntity
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

        public Guid ApplicationId
        {
            get; set;
        }

        public Guid DeviceBaseId
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