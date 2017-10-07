namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Infra.CrossCutting.Repository;

    public class ApplicationUser : IEntity<Guid>
    {
        #region Properties

        public Guid Id
        {
            get; set;
        }

        public Guid ApplicationId
        {
            get; set;
        }

        public Application Application { get; set; }

        public DateTime CreateDate { get; set; }

        #endregion Properties
    }
}